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
    public class ExtractController : ControllerBase
    {
        private readonly IOrganizationContextProvider _organizationContextProvider;
        private readonly IExtractService _extractService;

        public ExtractController(IOrganizationContextProvider organizationContextProvider, IExtractService extractService)
        {
            _organizationContextProvider = organizationContextProvider;
            _extractService = extractService;
        }

        // GET: api/Support
        [RequireCurrentOrganization]
        [RequirePermission(UserPermission.ViewMyOrganization)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExtractDto>>> GetSupportTickets()
        {
            var currentOrganizationId = await _organizationContextProvider.GetCurrentOrganizationId();
            return Ok(await _extractService.GetExtractsByOrganization(currentOrganizationId));
        }
    }
}