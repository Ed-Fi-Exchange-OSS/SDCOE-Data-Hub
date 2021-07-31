using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataHub.Api.Data;
using DataHub.Api.Data.Entities;
using DataHub.Api.Data.Models;
using DataHub.Api.Enums;
using Microsoft.EntityFrameworkCore;

namespace DataHub.Api.Services
{
    public interface IOrganizationService
    {
        Task<OrganizationDto> GetOrganization(int organizationId);
        Task<IEnumerable<OrganizationDto>> GetAllOrganizations();
    }

    public class OrganizationService : IOrganizationService
    {
        private readonly SDCOEDatahubContext _context;
        private readonly IMapper _mapper;

        public OrganizationService(IMapper mapper, SDCOEDatahubContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<OrganizationDto> GetOrganization(int organizationId)
        {
            var organizations = _context.Organization.Where(o => o.OrganizationId == organizationId);
            return await _mapper.ProjectTo<OrganizationDto>(organizations).SingleOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<OrganizationDto>> GetAllOrganizations()
        {
            var organizations = _context.Organization.AsQueryable();
            return await _mapper.ProjectTo<OrganizationDto>(organizations).ToListAsync().ConfigureAwait(false);
        }
    }
}