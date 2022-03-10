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
    public class SupportController : ControllerBase
    {
        private readonly IOrganizationContextProvider _organizationContextProvider;
        private readonly ISupportService _supportService;

        public SupportController(IOrganizationContextProvider organizationContextProvider, ISupportService supportService)
        {
            _organizationContextProvider = organizationContextProvider;
            _supportService = supportService;
        }

        // GET: api/Support
        [RequireCurrentOrganization]
        [RequirePermission(UserPermission.ViewMyOrganization)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupportDto>>> GetSupportTickets()
        {
            var currentOrganizationId = await _organizationContextProvider.GetCurrentOrganizationId();
            return Ok(await _supportService.GetSupportTicketsByOrganization(currentOrganizationId));
        }
    }
}