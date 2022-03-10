using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataHub.Api.Data;
using DataHub.Api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DataHub.Api.Services
{
    public interface IExtractService
    {
        Task<IEnumerable<ExtractDto>> GetExtractsByOrganization(int organizationId);
    }

    public class ExtractService : IExtractService
    {
        private readonly SDCOEDatahubContext _context;
        private readonly IMapper _mapper;

        public ExtractService(IMapper mapper, SDCOEDatahubContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<ExtractDto>> GetExtractsByOrganization(int organizationId)
        {
            var extracts = _context.Extract.Where(c => c.OrganizationId == organizationId);

            return await _mapper.ProjectTo<ExtractDto>(extracts).ToListAsync().ConfigureAwait(false);
        }
    }
}