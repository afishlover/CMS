using Client.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

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
		public async Task<IActionResult> Create(string courseId, string fileURL)
		{
			HttpContext.Request.Headers.TryGetValue("Authorization", out var token);
			if (string.IsNullOrEmpty(token))
			{
				return RedirectToAction("Login", "Account");
			}
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.ToString());

			var role = HttpContext.Session.GetString("Role");
			ViewData["role"] = role;

			string CourseId = courseId;
			DateTime OpenTime = DateTime.Now;
			DateTime CloseTime = DateTime.Now;
			string FileURL = fileURL;
			//validate

			CreateResourceDTO resource = new CreateResourceDTO
			{
				CourseId = Guid.Parse(CourseId),
				Content = "",
				OpenTime = OpenTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
				CloseTime = CloseTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
				FileURL = FileURL
			};
			string strData = JsonConvert.SerializeObject(resource);
			HttpContent content = new StringContent(strData, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await _client.PostAsync(CmsApiUrl + "/resource/CreateResource", content);
			if (response.IsSuccessStatusCode)
			{
				return RedirectToAction("Detail", "Course", new { id = "84e4cb70-c6e2-4703-821a-5fabe4203de6" });
				}


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
