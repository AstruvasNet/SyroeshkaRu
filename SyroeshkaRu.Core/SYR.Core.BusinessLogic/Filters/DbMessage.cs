using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SYR.Core.BusinessLogic.Filters {
	[AttributeUsage(AttributeTargets.Method)]
	public class DbMessage : ActionFilterAttribute {

		public void OnActionExecuting(ResourceExecutingContext context)
		{
			if (context.ModelState.IsValid) context.Result = new BadRequestObjectResult(context.ModelState);

//			if (Convert.ToInt32(Message.ToString().Split("//")[0]) == 1)
//			{
//				context.Result = new OkObjectResult(Message);
//			}
			
			//context.ModelState.AddModelError("System", Message.ToString().Split("//")[1]);

			context.Result = new BadRequestObjectResult(context.ModelState);
		}
	}
}