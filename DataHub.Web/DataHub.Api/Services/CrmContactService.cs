using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataHub.Api.Data;
using DataHub.Api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DataHub.Api.Services
{
    public interface ICrmContactService
    {
        Task<IEnumerable<CrmContactDto>> GetCrmContactsByOrganization(int organizationId);
    }

    public class CrmContactService : ICrmContactService
    {
        private readonly SDCOEDatahubContext _context;
        private readonly IMapper _mapper;

        public CrmContactService(IMapper mapper, SDCOEDatahubContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<CrmContactDto>> GetCrmContactsByOrganization(int organizationId)
        {
            var crmContacts = _context.CRMContact.Where(c => c.OrganizationId == organizationId);

            return await _mapper.ProjectTo<CrmContactDto>(crmContacts).ToListAsync().ConfigureAwait(false);
        }
    }
}