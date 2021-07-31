using System;
using DataHub.Api.Enums;

namespace DataHub.Api.Data.Models
{
	public class EdFiRequestDto
	{
		public int EdFiRequestId { get; set; }
		public string Description { get; set; }
		public DateTime? RequestDate { get; set; }
		public RequestStatus RequestStatus { get; set; }
		public bool IsArchived { get; set; }
	}
}