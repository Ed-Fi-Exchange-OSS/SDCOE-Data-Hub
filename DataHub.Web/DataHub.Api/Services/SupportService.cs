using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataHub.Api.Data;
using DataHub.Api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DataHub.Api.Services
{
    public interface ISupportService
    {
        Task<IEnumerable<SupportDto>> GetSupportTicketsByOrganization(int organizationId);
    }

    public class SupportService : ISupportService
    {
        private readonly SDCOEDatahubContext _context;
        private readonly IMapper _mapper;

        public SupportService(IMapper mapper, SDCOEDatahubContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<SupportDto>> GetSupportTicketsByOrganization(int organizationId)
        {
            var supportTickets = _context.Support.Where(c => c.OrganizationId == organizationId);

            return await _mapper.ProjectTo<SupportDto>(supportTickets).ToListAsync().ConfigureAwait(false);
        }
    }
}