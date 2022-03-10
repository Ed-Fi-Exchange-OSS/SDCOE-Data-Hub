using AutoMapper;
using DataHub.Api.Data;
using DataHub.Api.Data.Entities;
using DataHub.Api.Data.Models;
using DataHub.Api.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataHub.Api.Services
{
	public interface IEdFiRequestService
	{
        Task<IEnumerable<EdFiRequestDto>> GetActiveEdFiRequestsByOrganization(int organizationId);
        Task<EdFiRequestDto> AddEdFiRequestByOrganization(int organizationId, EdFiRequestDto request);
        Task<EdFiRequestDto> UpdateEdFiRequestByOrganization(int organizationId, EdFiRequestDto request);
    }
	public class EdFiRequestService : IEdFiRequestService
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IEmailService _emailService;
        private readonly SDCOEDatahubContext _context;
        public EdFiRequestService(IMapper mapper, IConfiguration config, IEmailService emailService, SDCOEDatahubContext context)
		{
            _mapper = mapper;
            _config = config;
            _emailService = emailService;
            _context = context;
        }

        public async Task<IEnumerable<EdFiRequestDto>> GetActiveEdFiRequestsByOrganization(int organizationId)
        {
            var requestStatus = new List<int> { (int)RequestStatus.Requested, (int)RequestStatus.InProgress, (int)RequestStatus.Completed, (int)RequestStatus.Denied };
            var requests = _context.EdFiRequest
                .Where(a => a.OrganizationId == organizationId 
                            && requestStatus.Contains((int)a.RequestStatus)
                            && !a.IsArchived);

            return await _mapper.ProjectTo<EdFiRequestDto>(requests).ToListAsync().ConfigureAwait(false);
        }

        public async Task<EdFiRequestDto> AddEdFiRequestByOrganization(int organizationId, EdFiRequestDto request)
		{
            var edfiRequest = _mapper.Map<EdFiRequest>(request);
            edfiRequest.OrganizationId = organizationId;
            edfiRequest.RequestStatus = (byte)RequestStatus.Requested;

            _context.Add<EdFiRequest>(edfiRequest);

            await _context.SaveChangesAsync().ConfigureAwait(false);

            await _context.Entry(edfiRequest).Reference(r => r.Organization).LoadAsync().ConfigureAwait(false);

            // This needs to come from configuration
            string emailTo = _config["SmtpSettings:EmailTo"];
            string emailToName = _config["SmtpSettings:EmailToName"];
                    
            string message = $@"<h4>New EdFi Request</h4>
                <p><b>{edfiRequest.Organization.OrganizationName}</b> has created a new request:</p>
                <p><i>{request.Description}</i></p>";

            await _emailService.SendMail(
                to: emailTo,
                toName: emailToName,
                subject: "SDCOE EdFi Request",
                html: message
            ).ConfigureAwait(false);

            return _mapper.Map<EdFiRequestDto>(edfiRequest);
        }

        public async Task<EdFiRequestDto> UpdateEdFiRequestByOrganization(int organizationId, EdFiRequestDto request)
        {
            var edfiRequest = await _context.EdFiRequest
                .Where(a => a.OrganizationId == organizationId && a.EdFiRequestId == request.EdFiRequestId)
                .SingleOrDefaultAsync().ConfigureAwait(false);

            if (edfiRequest == null)
			{
                throw new Exception("EdFiRequest was not found."); 
            }

            edfiRequest.IsArchived = request.IsArchived;
            edfiRequest.RequestStatus = (byte)request.RequestStatus;
            edfiRequest.LastModifiedOn = DateTime.Now;

            await _context.SaveChangesAsync().ConfigureAwait(false);

            return _mapper.Map<EdFiRequestDto>(edfiRequest);
        }
    }
}
