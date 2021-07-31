using DataHub.Api.Data.Models;
using DataHub.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataHub.Api.Authorization;
using DataHub.Api.Enums;

namespace DataHub.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EdFiRequestController : ControllerBase
	{
		private readonly IEdFiRequestService _edfiRequestService;
		private readonly IOrganizationContextProvider _organizationContextProvider;

		public EdFiRequestController(IOrganizationContextProvider organizationContextProvider, IEdFiRequestService edfiRequestService)
		{
			_organizationContextProvider = organizationContextProvider;
			_edfiRequestService = edfiRequestService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<EdFiRequestDto>>> GetActiveEdFiRequests()
		{
			var currentOrganizationId = await _organizationContextProvider.GetCurrentOrganizationId();
			return Ok(await _edfiRequestService.GetActiveEdFiRequestsByOrganization(currentOrganizationId));
		}

		[HttpGet]
		[Route("RequestType")]
		public async Task<ActionResult> GetEdFiRequestTypes()
		{ 
			var requestTypes = new List<object> {
				new Dictionary<string, string> { { "id", "1"}, { "description", "Set up a 3.4 Ed-Fi ODS with sample data for my district" } },
				new Dictionary<string, string> { { "id", "2"}, { "description", "Set up a new 2.6 Ed-Fi ODS for my district (to connect our SIS to)" } },
				new Dictionary<string, string> { { "id", "3"}, { "description", "Set up a new 3.4 Ed-Fi ODS for my district (to connect our SIS to)" } },
				new Dictionary<string, string> { { "id", "4"}, { "description", "Remove my 2.6 Ed-Fi ODS" } },
				new Dictionary<string, string> { { "id", "5"}, { "description", "Remove my 3.4 Ed-Fi ODS" } },
				new Dictionary<string, string> { { "id", "6"}, { "description", "Remove my 3.4 Ed-Fi ODS with sample data" } },
			};			   
			return Ok(requestTypes);
		}

		[RequirePermission(UserPermission.CanCreateEdFiRequest)]
		[HttpPost]
		public async Task<ActionResult<EdFiRequestDto>> PostEdFiRequest(EdFiRequestDto request)
		{
            if (request.IsArchived)
            {
                return BadRequest();
            }

			var currentOrganizationId = await _organizationContextProvider.GetCurrentOrganizationId();
			return Ok(await _edfiRequestService.AddEdFiRequestByOrganization(currentOrganizationId, request));
		}

        [RequirePermission(UserPermission.CanUpdateEdFiRequestStatus)]
		[HttpPut("{id}")]
		public async Task<ActionResult<EdFiRequestDto>> PutEdFiRequest(int id, EdFiRequestDto request)
		{
            if (id != request.EdFiRequestId)
            {
                return BadRequest();
            }

            var currentOrganizationId = await _organizationContextProvider.GetCurrentOrganizationId();
			return Ok(await _edfiRequestService.UpdateEdFiRequestByOrganization(currentOrganizationId, request));
		}
	}
}
