using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SYR.UserInterface.MVC.Controllers
{
	[Route("error")]
    public class ErrorController : Controller
    {
		[HttpGet("404")]
        public new async Task<IActionResult> NotFound()
        {
	        return await Task.Run(View);
        }
    }
}