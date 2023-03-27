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
		public async Task<IActionResult> Create(IFormFile file)
		{
			HttpContext.Request.Headers.TryGetValue("Authorization", out var token);
			if (string.IsNullOrEmpty(token))
			{
				return RedirectToAction("Login", "Account");
			}
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.ToString());

			var role = HttpContext.Session.GetString("Role");
			ViewData["role"] = role;

			// Tạo MultipartFormDataContent
			var formContent = new MultipartFormDataContent();

			CreateResourceDTO resource = new CreateResourceDTO
			{
				CourseId = Guid.Parse("84e4cb70-c6e2-4703-821a-5fabe4203de6"),
				Content = "",
				OpenTime = DateTime.Now,
				CloseTime = DateTime.Now,
			};
			string strData = JsonConvert.SerializeObject(resource);
			formContent.Add(new StringContent(strData, Encoding.UTF8, "multipart/form-data"), "resourceDTO");

			// Add file
			if (file == null || file.Length == 0)
				return Content("Tệp không được chọn hoặc tệp rỗng.");
			var fileContent = new StreamContent(file.OpenReadStream());
			fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
			formContent.Add(fileContent, "file", file.FileName);

			HttpResponseMessage response = await _client.PostAsync(CmsApiUrl + "/resource/CreateResource", formContent);
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
