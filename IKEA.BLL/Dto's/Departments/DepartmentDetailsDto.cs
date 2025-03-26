using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Dto_s.Departments
{
	public class DepartmentDetailsDto
	{
		public int Id { get; set; }

		#region Adminstrator
		public bool IsDeleted { get; set; } // Soft delete

		public int CreatedBy { get; set; }

		public DateTime CreatedOn { get; set; }

		public int LastModfiedOn { get; set; }

		public DateTime LastModifiedOn { get; set; }
		#endregion

		public string Name { get; set; } = null!;

		public string Code { get; set; } = null!;

		public string? Description { get; set; }

		public DateTime CreationDate { get; set; }
	}
}