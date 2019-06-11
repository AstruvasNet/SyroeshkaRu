using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SYR.Core.BusinessLogic.Helpers
{
	public static class ErrorHeplers
	{
		public static async Task<bool> ModelState(ModelStateDictionary modelState, object message)
		{
			if (modelState.IsValid)
			{
				if (Convert.ToInt32(message.ToString().Split("//")[0]) == 1)
				{
					return await Task.Run(() => true);
				}

				modelState.AddModelError("System", message.ToString().Split("//")[1]);
				return await Task.Run(() => false);
			}
			return await Task.Run(() => false);
		}
	}
}
