using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using SYR.Core.BusinessLogic.Helpers;
using SYR.Core.DomainModel.System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SYR.Core.BusinessLogic.ViewModel
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Поле {0} не должно быть пустым")]
		[Display(Name = "Логин", Prompt = "Логин")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "Поле {0} не должно быть пустым")]
		[Display(Name = "Пароль", Prompt = "Пароль")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name = "Запомнить")]
		public bool RememberMe { get; set; }

		public string ReturnUrl { get; set; }
	}

	public class RegisterViewModel
	{
		[Required(ErrorMessage = "Поле {0} не должно быть пустым")]
		[Display(Name = "Логин", Prompt = "Логин")]
		public string UserName { get; set; }

		[EmailValidate]
		[Display(Name = "E-mail", Prompt = "E-mail")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Поле {0} не должно быть пустым")]
		[Display(Name = "Имя", Prompt = "Имя")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Поле {0} не должно быть пустым")]
		[Display(Name = "Фамилия", Prompt = "Фамилия")]
		public string LastName { get; set; }

		[Display(Name = "Отчество", Prompt = "Отчество")]
		public string SecondName { get; set; }

		[Required(ErrorMessage = "Поле {0} не должно быть пустым")]
		[Display(Name = "Номер телефона", Prompt = "Номер телефона")]
		[RegularExpression(@"^\d{10}$", ErrorMessage = "{0} имеет неверный формат")]
		public string PhoneNumber { get; set; }

		[Required(ErrorMessage = "Поле {0} не должно быть пустым")]
		[Display(Name = "Пароль", Prompt = "Пароль")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Поле {0} не должно быть пустым")]
		[Display(Name = "Подтверждение пароля", Prompt = "Подтверждение пароля")]
		[Compare("Password", ErrorMessage = "Пароли не совпадают")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
	}

	public class UsersViewModel : IdentityUser
	{
		public ICollection<Roles> Roles { get; set; }
		public ICollection<string> UserRole { get; set; }
		public IEnumerable<SelectListItem> RolesCollection { get; set; }

		private UsersViewModel()
		{
			Roles = new List<Roles>();
			UserRole = new List<string>();
			RolesCollection = Roles.Select(m => new SelectListItem
			{
				Text = m.Name,
				Value = m.Id
			}).OrderBy(i => i.Value);
		}
	}
}