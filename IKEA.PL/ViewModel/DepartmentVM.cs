using IKEA.DAL.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.ViewModel
{
	public class DepartmentVM
	{
		


		public int Id { get; set; }

		[Required(ErrorMessage = "Name is required")]

		public string Name { get; set; } = null!;
		[Required(ErrorMessage = "Code is required")]

		public string Code { get; set; } = null!;

		public string? Description { get; set; }

		[Display(Name = "Data of creation")]
		public DateTime CreationDate { get; set; }
	}
}
