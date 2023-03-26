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

		[HttpGet]
		[Route("course/mine")]
		public async Task<IActionResult> MyCourses()
		{
			HttpContext.Request.Headers.TryGetValue("Authorization", out var token);
			if (string.IsNullOrEmpty(token))
			{
				return RedirectToAction("Login", "Account");
			}
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());

			var role = HttpContext.Session.GetString("Role");
			ViewData["role"] = role;

			if (role.Equals("Teacher"))
			{
				//Get all cates
				HttpResponseMessage response = await _client.GetAsync(CmsApiUrl + "/category/GetAllCategories");
				List<RootCategoryDTO> categories = new List<RootCategoryDTO>();
				if (response.IsSuccessStatusCode)
				{
					// Get the categories list from response
					var result = await response.Content.ReadAsStringAsync();
					categories = JsonConvert.DeserializeObject<List<RootCategoryDTO>>(result);
				}
				if (categories != null)
				{
					ViewData["categories"] = categories;
				}


				//get courses where teacher is logged user
				string teacherId = HttpContext.Session.GetString("AccountId");
				response = await _client.GetAsync(CmsApiUrl + "/course/GetCoursesByTeacherId?teacherId=" + teacherId);
				if (response.IsSuccessStatusCode)
				{
					var result = await response.Content.ReadAsStringAsync();
					List<CourseDTO> courses = JsonConvert.DeserializeObject<List<CourseDTO>>(result);
					if (courses == null)
					{
						courses = new List<CourseDTO>();
					}
					ViewData["courses"] = courses;
				}

			}
			else
			{
				//get all enrolled courses
				string studentId = HttpContext.Session.GetString("AccountId");
				HttpContent content = new StringContent(studentId, Encoding.UTF8, "application/json");
				HttpResponseMessage response = await _client.PostAsync(CmsApiUrl + "/course/GetStudentCoursesByStudentId", content);
				if (response.IsSuccessStatusCode)
				{
					var result = await response.Content.ReadAsStringAsync();
					List<StudentCourseDTO> courses = JsonConvert.DeserializeObject<List<StudentCourseDTO>>(result);
					if (courses == null)
					{
						courses = new List<StudentCourseDTO>();
					}
					ViewData["studentCourses"] = courses;
				}
			}

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
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());

			var role = HttpContext.Session.GetString("Role");
			ViewData["role"] = role;

			string strData = JsonConvert.SerializeObject(course);
			HttpContent content = new StringContent(strData, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await _client.PostAsync(CmsApiUrl + "/course/CreateCourse", content);
			if (response.IsSuccessStatusCode)
			{
				return RedirectToAction("Detail", "Category", new { id = CategoryId });
			}

			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public async Task<IActionResult> Edit(IFormCollection form)
		{
			string CourseName = form["courseName"];
			string CourseCode = form["courseCode"];
			DateTime StartDate = DateTime.Parse(form["startDate"]);
			DateTime EndDate = DateTime.Parse(form["endDate"]);
			string CategoryName = form["categoryName"];
			Guid CourseId = Guid.Parse(form["courseId"]);
			//validate

			UpdateCourseDTO course = new UpdateCourseDTO
			{
				CourseId = CourseId,
				CourseName = CourseName,
				CourseCode = CourseCode,
				StartDate = StartDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
				EndDate = EndDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
				CategoryName = CategoryName
			};

			HttpContext.Request.Headers.TryGetValue("Authorization", out var token);
			if (string.IsNullOrEmpty(token))
			{
				return RedirectToAction("Login", "Account");
			}
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());

			var role = HttpContext.Session.GetString("Role");
			ViewData["role"] = role;

			string strData = JsonConvert.SerializeObject(course);
			HttpContent content = new StringContent(strData, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await _client.PutAsync(CmsApiUrl + "/course/UpdateCourse", content);
			if (response.IsSuccessStatusCode)
			{
				return RedirectToAction("Detail", "Course", CourseId);
			}

			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		[Route("course/{id}")]
		public async Task<IActionResult> Detail(string id)
		{
			HttpContext.Request.Headers.TryGetValue("Authorization", out var token);
			if (string.IsNullOrEmpty(token))
			{
				return RedirectToAction("Login", "Account");
			}
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());

			var role = HttpContext.Session.GetString("Role");
			ViewData["role"] = role;


			//Get current course details
			CourseDTO current;
			HttpResponseMessage response = await _client.GetAsync(CmsApiUrl + "/course/GetCourseById/" + id);
			if (response.IsSuccessStatusCode)
			{
				// Get the categories list from response
				var result = await response.Content.ReadAsStringAsync();
				current = JsonConvert.DeserializeObject<CourseDTO>(result);
				if (current == null)
				{
					//Error: can not found category
					return RedirectToAction("Index", "Category");
				}
				ViewData["currentCourse"] = current;
			}

			// Get students
			response = await _client.GetAsync(CmsApiUrl + "/Student/GetStudentsByCourseId/" + id);
			if (response.IsSuccessStatusCode)
			{
				// Get the categories list from response
				var result = await response.Content.ReadAsStringAsync();
				List<StudentDTO> students = JsonConvert.DeserializeObject<List<StudentDTO>>(result);
				if (students == null)
				{
					students = new List<StudentDTO>();
				}
				ViewData["students"] = students;
			}


			//Get resources
			//response = await _client.GetAsync(CmsApiUrl + "/course/GetCourseByCategoryId/" + id);
			//if (response.IsSuccessStatusCode)
			//{
			//	// Get the categories list from response
			//	var result = await response.Content.ReadAsStringAsync();
			//	List<CourseDTO> courses = JsonConvert.DeserializeObject<List<CourseDTO>>(result);
			//	if (courses == null)
			//	{
			//		courses = new List<CourseDTO>();
			//	}
			//	ViewData["courses"] = courses;
			//}

			//check enrolled
			if (role.Equals("Student"))
			{
				response = await _client.GetAsync(CmsApiUrl + "/course/CheckStudentEnrollCourse/" + id);
				if (response.IsSuccessStatusCode)
				{
					var result = await response.Content.ReadAsStringAsync();
					if (JsonConvert.DeserializeObject(result).Equals("Enrolled"))
					{
						ViewData["enrolled"] = true;
					}
				}
				ViewData["enrolled"] = false;
			}


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
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());

			var role = HttpContext.Session.GetString("Role");
			ViewData["role"] = role;


			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Enroll(string id)
		{
			HttpContext.Request.Headers.TryGetValue("Authorization", out var token);
			if (string.IsNullOrEmpty(token))
			{
				return RedirectToAction("Login", "Account");
			}
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());

			var role = HttpContext.Session.GetString("Role");
			ViewData["role"] = role;


			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Unenroll(string id)
		{

			HttpContext.Request.Headers.TryGetValue("Authorization", out var token);
			if (string.IsNullOrEmpty(token))
			{
				return RedirectToAction("Login", "Account");
			}
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());

			var role = HttpContext.Session.GetString("Role");
			ViewData["role"] = role;


			return View();
		}
	}
}
