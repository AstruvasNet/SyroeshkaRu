using Microsoft.AspNetCore.Mvc;
using SYR.Core.BusinessLogic.ViewModel;

namespace SYR.UserInterface.MVC.Areas.Identity.Components
{
	public class LoginFormComonent : ViewComponent
	{
		private readonly LoginViewModel _login = new LoginViewModel();
		//public LoginFormComonent(UserManager<Users> login)
		//{
		//	_login = login;
		//}

		public IViewComponentResult Invoke() => View("~/Areas/Identity/Views/Components/LoginForm.cshtml", _login);
	}
}