using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
	public class ResourceController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
