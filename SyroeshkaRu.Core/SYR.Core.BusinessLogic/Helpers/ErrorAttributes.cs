using SYR.Core.BusinessLogic.Common;
using SYR.Core.DomainModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace SYR.Core.BusinessLogic.Helpers
{
	public class TrueFalseBoolAttribute : ValidationAttribute
	{
		public override bool IsValid(Object value)
		{
			return value is bool;
		}
	}

	public class EmailValidate : ValidationAttribute
	{
		private string _user;
		private Enum _errorNo;

		public override bool IsValid(object value)
		{
			using (var user = new ModelContext())
			{
				if (value == null)
				{
					_errorNo = ModelError.Empty;
					return false;
				}
				if (user.Users.Any(i => i.Email == value.ToString()))
				{
					_user = user.Users.FirstOrDefault(i => i.Email == value.ToString())?.Email == null
						? null
						: user.Users.FirstOrDefault(i => i.Email == value.ToString())?.Email;

					_errorNo = ModelError.Distinct;
					return false;
				}

				if (!Regex.IsMatch(value.ToString(),
					@"(\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)", RegexOptions.IgnoreCase))
				{
					_errorNo = ModelError.RegularExpression;
					return false;
				}

				return true;
			}
		}

		public override string FormatErrorMessage(string name)
		{
			return $"Значение поля {name} {_user} {_errorNo.GetEnum().ToLower()}";
		}
	}
}
