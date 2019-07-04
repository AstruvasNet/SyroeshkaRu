using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SYR.Core.BusinessLogic.ViewModel;
using SYR.Core.DomainModel.System;
using System.Threading.Tasks;

namespace SYR.UserInterface.MVC.Areas.Identity.Controllers
{
	[Area("Identity")]
	public class AccountController : Controller
	{
		private readonly UserManager<Users> _userManager;
		private readonly SignInManager<Users> _signInManager;

		public AccountController(UserManager<Users> userManager, SignInManager<Users> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public async Task<IActionResult> Register()
		{
			if (!_signInManager.IsSignedIn(User))
			{
				return await Task.Run(View);
			}

			return await Task.Run(() => RedirectToAction("Index", new { Controller = "Home" }));
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				Users user = new Users
				{
					Email = model.Email,
					UserName = model.UserName,
					FirstName = model.FirstName,
					LastName = model.LastName,
					SecondName = model.SecondName,
					PhoneNumber = model.PhoneNumber
				};

				var result = await _userManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
				{
					await _signInManager.SignInAsync(user, false);
					await _userManager.AddToRoleAsync(user, "customer");
					return RedirectToAction("Index", "Home");
				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}
			return View(model);
		}

		[
			HttpGet
		]
		public async Task<IActionResult> Login()
		{
			if (!_signInManager.IsSignedIn(User))
			{
				return await Task.Run(View);
			}

			return await Task.Run(() => RedirectToAction("Index", new { Controller = "Home" }));
		}

		[
			HttpPost,
			ValidateAntiForgeryToken
		]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);
			var result =
				await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
			if (result.Succeeded)
			{
				if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
				{
					return Ok(model.ReturnUrl);
				}

				return Ok(model);
			}
			ModelState.AddModelError("", "Неправильный логин и (или) пароль");
			return BadRequest(ModelState);
		}

		[
			HttpPost,
			ValidateAntiForgeryToken
		]
		public async Task<IActionResult> LogOff()
		{
			await _signInManager.SignOutAsync();
			return Ok(new { request = "/" });
		}

		public IActionResult AccessDenied()
		{
			return View();
		}
	}
}