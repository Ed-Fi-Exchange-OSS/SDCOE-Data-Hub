using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataHub.Api.Data;
using DataHub.Api.Data.Models;
using DataHub.Api.Enums;
using Microsoft.EntityFrameworkCore;

namespace DataHub.Api.Services
{
    public interface IAnnouncementService
    {
        Task<IEnumerable<AnnouncementDto>> GetActiveAnnouncementsByOrganization(int organizationId);
    }

    public class AnnouncementService : IAnnouncementService
    {
        private readonly SDCOEDatahubContext _context;
        private readonly IMapper _mapper;

        public AnnouncementService(IMapper mapper, SDCOEDatahubContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<AnnouncementDto>> GetActiveAnnouncementsByOrganization(int organizationId)
        {
            var currentDate = DateTime.Now;
            var announcements = _context.Announcement
                .Where(a => a.OrganizationId == organizationId &&
                            a.Status == (int) RecordStatus.Active &&
                            (!a.DisplayUntilDate.HasValue || a.DisplayUntilDate >= currentDate));

            return await _mapper.ProjectTo<AnnouncementDto>(announcements).ToListAsync().ConfigureAwait(false);
        }
    }
}