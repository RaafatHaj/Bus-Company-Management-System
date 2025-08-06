using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Infrastructure.Persistence.Entities;

namespace TravelCompany.Domain.Common
{
	public class BaseEntity
	{
		public bool IsDeleted { get; set; }
		public string? CreatedById { get; set; }
		public ApplicationUser? CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }=DateTime.Now;
		public string? LastUpdatedById { get; set; }
		public ApplicationUser? LastUpdatedBy { get; set; }
		public DateTime? LastUpdatedOn { get; set; }
	}
}
