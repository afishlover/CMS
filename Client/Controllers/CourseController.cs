using Client.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;

namespace Client.Controllers
{
	public class CourseController : Controller
	{
		private readonly HttpClient _client;

		private readonly IConfiguration _configuration;
		private string CmsApiUrl;

		public CourseController(HttpClient client, IConfiguration configuration)
		{
			_client = client;
			_configuration = configuration;
			CmsApiUrl = _configuration.GetSection("CmsApiRoot").Value;
		}
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(IFormCollection form)
		{
			string CourseName = form["courseName"];
			string CourseCode = form["courseCode"];
			DateTime StartDate = DateTime.Parse(form["startDate"]);
			DateTime EndDate = DateTime.Parse(form["endDate"]);
			Guid CategoryId = Guid.Parse(form["categoryId"]);
			//validate

			CreateCourseDTO course = new CreateCourseDTO
			{
				CourseName = CourseName,
				CourseCode = CourseCode,
				StartDate = StartDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
				EndDate = EndDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
				CategoryId = CategoryId
			};

			HttpContext.Request.Headers.TryGetValue("Authorization", out var token);
			if (string.IsNullOrEmpty(token))
			{
				return RedirectToAction("Login", "Account");
			}
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.ToString());

			string strData = JsonConvert.SerializeObject(course);
			HttpContent content = new StringContent(strData, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await _client.PostAsync(CmsApiUrl + "/course/CreateCourse", content);
			if (response.IsSuccessStatusCode)
			{
				return RedirectToAction("Detail", "Category", CategoryId);
			}

			return RedirectToAction("Index", "Home");
		}
	}
}
