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
    public class CrmContactController : ControllerBase
    {
        private readonly IOrganizationContextProvider _organizationContextProvider;
        private readonly ICrmContactService _crmContactService;

        public CrmContactController(IOrganizationContextProvider organizationContextProvider, ICrmContactService crmContactService)
        {
            _organizationContextProvider = organizationContextProvider;
            _crmContactService = crmContactService;
        }

        // GET: api/CrmContact
        [RequireCurrentOrganization]
        [RequirePermission(UserPermission.ViewMyOrganization)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CrmContactDto>>> GetCrmContacts()
        {
            var currentOrganizationId = await _organizationContextProvider.GetCurrentOrganizationId();
            return Ok(await _crmContactService.GetCrmContactsByOrganization(currentOrganizationId));
        }
    }
}