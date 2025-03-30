using IKEA.DAL.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.ViewModel
{
	public class EmployeeVM
	{
		public int Id { get; set; }	
		[MaxLength(50, ErrorMessage = "Max length is 50 characters")]
		[MinLength(5, ErrorMessage = "Min length is 5 characters")]
		public string Name { get; set; }

		[Range(22, 30)]
		public int? Age { get; set; }

		[RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$", ErrorMessage = "Address must be like 123-Street-City-Country")]
		public string? Address { get; set; }

		public decimal Salary { get; set; }

		[Display(Name = "Is Active")]
		public bool IsActive { get; set; }

		[EmailAddress]
		public string? Email { get; set; }
		[Display(Name = "Phone Number")]
		[Phone]
		public string? PhoneNumber { get; set; }

		[Display(Name = "Hiring Date")]
		public DateOnly HiringDate { get; set; }

		public Gender Gender { get; set; }

		public EmployeeType EmployeeType { get; set; }
	}
}
