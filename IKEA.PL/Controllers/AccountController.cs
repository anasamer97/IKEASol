﻿using IKEA.DAL.Identity;
using IKEA.PL.ViewModel.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IKEA.PL.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
		}


		[HttpGet]
		public IActionResult SignUp()
		{
			return View();
		}


		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel signUpViewModel)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			var User = await userManager.FindByNameAsync(signUpViewModel.UserName);

			if (User is not null)
			{
				ModelState.AddModelError(nameof(SignUpViewModel.UserName), "this username is already in use for other account");
				return View(signUpViewModel);
			} 
			User = new ApplicationUser()
			{
				FName = signUpViewModel.FirstName,
				LName = signUpViewModel.LastName,
				UserName = signUpViewModel.UserName,
				Email = signUpViewModel.Email,
				IsAgree = signUpViewModel.IsAgree,
			};

			var Result = await userManager.CreateAsync(User, signUpViewModel.Password);

			if (Result.Succeeded)
				RedirectToAction(nameof(LogIn));

			foreach (var error in Result.Errors)
				ModelState.AddModelError(string.Empty, error.Description);

			return View(signUpViewModel);

	
		}

		[HttpGet]
		public IActionResult LogIn()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> LogIn(LogInViewModel logInViewModel)
		{
			if (!ModelState.IsValid)
				BadRequest();

			var User = await userManager.FindByEmailAsync(logInViewModel.Email);

			if (User is not null)
			{
				var result = await signInManager.PasswordSignInAsync(User, logInViewModel.Password,logInViewModel.RememberMe, true);

				if(result.IsNotAllowed)
				{
					ModelState.AddModelError(string.Empty, "Your account is not confirmed");
				}

				if (result.IsLockedOut)
					ModelState.AddModelError(string.Empty, "Your Account Is Locked!");

				if (result.Succeeded)
					return RedirectToAction(nameof(HomeController.Index), "Home");
			}

			ModelState.AddModelError(String.Empty, "Invalid Login Attempt!");
			return View(logInViewModel);
		}

		public async Task<IActionResult> SignOut()
		{
			await signInManager.SignOutAsync();
			return RedirectToAction(nameof(LogIn));
		}
	}
}
