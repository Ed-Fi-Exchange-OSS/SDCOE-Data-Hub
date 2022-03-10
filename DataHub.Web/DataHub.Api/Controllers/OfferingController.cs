using System;
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
    public class OfferingController : ControllerBase
    {
        private readonly IOfferingService _offeringService;
        private readonly IOrganizationContextProvider _organizationContextProvider;

        public OfferingController(IOrganizationContextProvider organizationContextProvider,
            IOfferingService offeringService)
        {
            _organizationContextProvider = organizationContextProvider;
            _offeringService = offeringService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OfferingDto>>> GetAllOfferings()
        {
            return Ok(await _offeringService.GetAllOfferings());
        }

        [RequireCurrentOrganization]
        [RequirePermission(UserPermission.ViewMyOrganization)]
        [HttpGet]
        [Route("available")]
        public async Task<ActionResult<IEnumerable<OfferingDto>>> GetAvailableOfferingsOrSaaS()
        {
            var currentOrganizationId = await _organizationContextProvider.GetCurrentOrganizationId();
            return Ok(await _offeringService.GetAvailableOfferings(currentOrganizationId));
        }

        [RequireCurrentOrganization]
        [RequirePermission(UserPermission.ViewMyOrganization)]
        [HttpGet]
        [Route("participating")]
        public async Task<ActionResult<IEnumerable<OfferingDto>>> GetParticipatingOfferingsOrSaaS()
        {
            var currentOrganizationId = await _organizationContextProvider.GetCurrentOrganizationId();
            return Ok(await _offeringService.GetParticipatingOfferings(currentOrganizationId));
        }

        [RequireCurrentOrganization]
        [RequirePermission(UserPermission.CanCreateParticipation)]
        [HttpPut]
        [Route("participating/{itemNo}")]
        public async Task<IActionResult> CreateOfferingParticipation(int itemNo)
        {
            var currentOrganizationId = await _organizationContextProvider.GetCurrentOrganizationId();
            var createResult = await _offeringService.AddParticipation(currentOrganizationId, itemNo);
            if (!createResult)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [RequireCurrentOrganization]
        [RequirePermission(UserPermission.CanCreateParticipation)]
        [HttpDelete]
        [Route("participating/{itemNo}")]
        public async Task<IActionResult> DeleteOfferingParticipation(int itemNo)
        {
            var currentOrganizationId = await _organizationContextProvider.GetCurrentOrganizationId();
            var deleteResult = await _offeringService.DeleteParticipation(currentOrganizationId, itemNo);
            if (!deleteResult)
            {
                return BadRequest();
            }
            return NoContent();
        }

    }
}