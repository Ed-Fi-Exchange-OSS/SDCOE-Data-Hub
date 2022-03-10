using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataHub.Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace DataHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EdFiOdsController : Controller
    {
        private readonly IEdFiOdsService _edFiOdsService;
        private readonly IOrganizationContextProvider _organizationContextProvider;

        public EdFiOdsController(IEdFiOdsService edFiOdsService, IOrganizationContextProvider organizationContextProvider)
        {
            _edFiOdsService = edFiOdsService;
            _organizationContextProvider = organizationContextProvider;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var orgId = await _organizationContextProvider.GetCurrentOrganizationId().ConfigureAwait(false);
            return Ok(await _edFiOdsService.GetAllOdsStatusesByOrganization(orgId).ConfigureAwait(false));
        }
    }
}
