using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.ViewModel.Identity
{
	public class SignUpViewModel
	{
		[Display(Name = "First Name")]
		public string FirstName { get; set; } = null!;

		[Display(Name = "Last Name")]
		public string LastName { get; set; } = null!;

		public string UserName { get; set; } = null!;

		[EmailAddress]
		public string Email { get; set; } = null!;

		[DataType(DataType.Password)]
		public string Password { get; set; } = null!;

		[DataType(DataType.Password)]
		[Display(Name = "Confirm Password")]
		[Compare("Password", ErrorMessage = "Confirm password does not match with password")]
		public string ConfirmPassword { get; set; } = null!;


		[Display(Name = "Is Agree")]
		public bool IsAgree { get; set; } 
	}
}
