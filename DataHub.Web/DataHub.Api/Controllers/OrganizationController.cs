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
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;
        private readonly IOrganizationContextProvider _organizationContextProvider;

        public OrganizationController(IOrganizationContextProvider organizationContextProvider, IOrganizationService organizationService)
        {
            _organizationService = organizationService;
            _organizationContextProvider = organizationContextProvider;
        }

        // GET: api/Organization
        [RequirePermission(UserPermission.ViewAllOrganizations)]
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IEnumerable<OrganizationDto>>> GetAllOrganizations()
        {
            return Ok(await _organizationService.GetAllOrganizations());
        }

        [RequirePermission(UserPermission.ViewMyOrganization)]
        [RequireCurrentOrganization]
        [HttpGet]
        public async Task<ActionResult<OrganizationDto>> GetOrganization()
        {
            var currentOrgId = await _organizationContextProvider.GetCurrentOrganizationId().ConfigureAwait(false);

            return Ok(await _organizationService.GetOrganization(currentOrgId));
        }
    }
}