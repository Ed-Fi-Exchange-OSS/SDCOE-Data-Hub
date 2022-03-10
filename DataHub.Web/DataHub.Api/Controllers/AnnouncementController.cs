using System.Collections.Generic;
using System.Threading.Tasks;
using DataHub.Api.Authorization;
using DataHub.Api.Data.Models;
using DataHub.Api.Enums;
using DataHub.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        private readonly IOrganizationContextProvider _organizationContextProvider;
        private readonly IAnnouncementService _announcementService;

        public AnnouncementController(IOrganizationContextProvider organizationContextProvider, IAnnouncementService announcementService)
        {
            _organizationContextProvider = organizationContextProvider;
            _announcementService = announcementService;
        }

        // GET: api/Announcement
        [RequireCurrentOrganization]
        [RequirePermission(UserPermission.ViewMyOrganization)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnnouncementDto>>> GetActiveAnnouncements()
        {
            var currentOrganizationId = await _organizationContextProvider.GetCurrentOrganizationId();
            return Ok(await _announcementService.GetActiveAnnouncementsByOrganization(currentOrganizationId));
        }
    }
}