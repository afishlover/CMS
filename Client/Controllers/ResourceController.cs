using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Client.Controllers
{
	public class ResourceController : Controller
	{
		private readonly HttpClient _client;

		private readonly IConfiguration _configuration;
		private string CmsApiUrl;

		public ResourceController(HttpClient client, IConfiguration configuration)
		{
			_client = client;
			_configuration = configuration;
			CmsApiUrl = _configuration.GetSection("CmsApiRoot").Value;
		}

		[HttpPost]
		public async Task<IActionResult> Create()
		{
			HttpContext.Request.Headers.TryGetValue("Authorization", out var token);
			if (string.IsNullOrEmpty(token))
			{
				return RedirectToAction("Login", "Account");
			}
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.ToString());

			var role = HttpContext.Session.GetString("Role");
			ViewData["role"] = role;


			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Edit(string id)
		{
			HttpContext.Request.Headers.TryGetValue("Authorization", out var token);
			if (string.IsNullOrEmpty(token))
			{
				return RedirectToAction("Login", "Account");
			}
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.ToString());

			var role = HttpContext.Session.GetString("Role");
			ViewData["role"] = role;


			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Delete(string id)
		{
			HttpContext.Request.Headers.TryGetValue("Authorization", out var token);
			if (string.IsNullOrEmpty(token))
			{
				return RedirectToAction("Login", "Account");
			}
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.ToString());

			var role = HttpContext.Session.GetString("Role");
			ViewData["role"] = role;


			return View();
		}
	}
}
