using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataHub.Api.Data;
using DataHub.Api.Data.Entities;
using DataHub.Api.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DataHub.Api.Services
{
    public interface IOfferingService
    {
        Task<bool> AddParticipation(int organizationId, int itemNo);
        Task<bool> DeleteParticipation(int organizationId, int itemNo);
        Task<IEnumerable<OfferingDto>> GetAllOfferings();
        Task<IEnumerable<OfferingDto>> GetAvailableOfferings(int organizationId);
        Task<IEnumerable<OfferingDto>> GetParticipatingOfferings(int organizationId);
    }

    public class OfferingService : IOfferingService
    {
        private readonly IMapper _mapper;
        private readonly SDCOEDatahubContext _context;
        private readonly IEmailService _emailService;
        private readonly ILogger _logger;
        private readonly bool _sendEmailToContact;

        public OfferingService(IMapper mapper, SDCOEDatahubContext context, IEmailService emailService, IConfiguration configuration, ILogger<OfferingService> logger)
        {
            _mapper = mapper;
            _context = context;
            _emailService = emailService;
            _logger = logger;

            bool.TryParse(configuration["OfferingsSettings:SendEmailOnParticipation"], out _sendEmailToContact);
        }

        public async Task<bool> AddParticipation(int organizationId, int itemNo)
        {
            var orgOfferingStatus = await _context.Offering.Where(o => o.ItemNo == itemNo)
                .Select(o => new
                {
                    o.OfferingId,
                    o.ItemName,
                    o.ContactName,
                    o.ContactEmail,
                    ExistingParticipation = o.Participations.Any(p => p.OrganizationId == organizationId)
                }).SingleOrDefaultAsync().ConfigureAwait(false);
            if (orgOfferingStatus == null)
            {
                _logger.LogWarning($"Unable to add participation for organization id '{organizationId}' with item no '{itemNo}'. Item no doesn't exist.");
                return false;
            }
            if (orgOfferingStatus.ExistingParticipation)
            {
                _logger.LogWarning($"Unable to add participation for organization id '{organizationId}' with item no '{itemNo}'. Participation already exists.");
                return false;
            }

            var participation = new Participation
            {
                OrganizationId = organizationId,
                OfferingId = orgOfferingStatus.OfferingId,
                AsOfDate = DateTime.Now
            };
            _context.Add(participation);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            // Attempt to email, but don't fail if this doesn't succeed
            try
            {

                if (_sendEmailToContact && !string.IsNullOrWhiteSpace(orgOfferingStatus.ContactEmail))
                {
                    var organization = await _context.Organization
                        .Include(o => o.CRMContacts)
                        .SingleAsync(o => o.OrganizationId == organizationId).ConfigureAwait(false);

                    var message = $@"
<h4>New Participation Request</h4>
<p><b>{organization.OrganizationName}</b> has requested participation in {orgOfferingStatus.ItemName}.</p>
<h5>Contacts for this organization include:</h5>
<ul>
{string.Join(' ', organization.CRMContacts.Select(c => $"<li><ul><li>Name: {c.ContactName}</li><li>Title: {c.ContactTitle}</li><li>Email: {c.ContactEmail}</li><li>Phone: {c.ContactPhone}</li></ul></li>"))}
</ul>";

                    await _emailService.SendMail(orgOfferingStatus.ContactEmail, orgOfferingStatus.ContactName,
                            $"Request for participation in {orgOfferingStatus.ItemName}", message)
                        .ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Unable to send email for organization id '{organizationId}' to contact for item no '{itemNo}'.");
            }

            return true;
        }

        public async Task<bool> DeleteParticipation(int organizationId, int itemNo)
        {
            var matchingParticipation = await _context.Participation.SingleOrDefaultAsync(p => p.OrganizationId == organizationId && p.Offering.ItemNo == itemNo).ConfigureAwait(false);
            if (matchingParticipation == null)
            {
                _logger.LogWarning($"Unable to delete participation for organization id '{organizationId}' with item no '{itemNo}'. No existing participation was found to delete.");
                return false;
            }

            _context.Participation.Remove(matchingParticipation);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }

        public async Task<IEnumerable<OfferingDto>> GetAllOfferings()
        {
            var offerings = _context.Offering.AsQueryable();
            return await _mapper.ProjectTo<OfferingDto>(offerings).ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<OfferingDto>> GetAvailableOfferings(int organizationId)
        {
            return await GetOfferingsByOrganization(organizationId, false).ConfigureAwait(false);
        }

        public async Task<IEnumerable<OfferingDto>> GetParticipatingOfferings(int organizationId)
        {
            return await GetOfferingsByOrganization(organizationId, true).ConfigureAwait(false);
        }

        private async Task<IEnumerable<OfferingDto>> GetOfferingsByOrganization(int organizationId,
            bool isEnrolledFilter)
        {
            var offerings = _context.Offering.Where(o =>
                isEnrolledFilter == o.Participations.Any(p => p.OrganizationId == organizationId));

            return await _mapper.ProjectTo<OfferingDto>(offerings).ToListAsync().ConfigureAwait(false);
        }
    }
}