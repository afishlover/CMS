using Client.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace Client.Controllers
{
	public class CategoryController : Controller
	{
		private readonly HttpClient _client;

		private readonly IConfiguration _configuration;
		private string CmsApiUrl;

		public CategoryController(HttpClient client, IConfiguration configuration)
		{
			_client = client;
			_configuration = configuration;
			CmsApiUrl = _configuration.GetSection("CmsApiRoot").Value;
		}

		[HttpGet]
		[Route("/categories")]
		public async Task<IActionResult> Index()
		{
			HttpContext.Request.Headers.TryGetValue("Authorization", out var token);
			if (string.IsNullOrEmpty(token))
			{
				return RedirectToAction("Login", "Account");
			}
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.ToString());


			HttpResponseMessage response = await _client.GetAsync(CmsApiUrl + "/category/GetAllRootCategories");
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
			return View();
		}

		[HttpGet]
		[Route("category/{id}")]
		public async Task<IActionResult> Detail(string id)
		{
			HttpContext.Request.Headers.TryGetValue("Authorization", out var token);
			if (string.IsNullOrEmpty(token))
			{
				return RedirectToAction("Login", "Account");
			}
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.ToString());



			//Get current category details
			RootCategoryDTO current;
			HttpResponseMessage response = await _client.GetAsync(CmsApiUrl + "/category/GetCategoryId/" + id);
			if (response.IsSuccessStatusCode)
			{
				// Get the categories list from response
				var result = await response.Content.ReadAsStringAsync();
				current = JsonConvert.DeserializeObject<RootCategoryDTO>(result);
				if (current == null)
				{
					//Error: can not found category
					return RedirectToAction("Index", "Category");
				}
				ViewData["current"] = current;
			}

			//Get subcategory
			response = await _client.GetAsync(CmsApiUrl + "/category/GetAllChildCategoriesByParentId/"+id);
			if (response.IsSuccessStatusCode)
			{
				// Get the categories list from response
				var result = await response.Content.ReadAsStringAsync();
				List<RootCategoryDTO> categories = JsonConvert.DeserializeObject<List<RootCategoryDTO>>(result);
				if (categories == null)
				{
					categories = new List<RootCategoryDTO>();
				}
				ViewData["categories"] = categories;
			}


			//Get courses
			response = await _client.GetAsync(CmsApiUrl + "/course/GetCourseByCategoryId/"+id);
			if (response.IsSuccessStatusCode)
			{
				// Get the categories list from response
				var result = await response.Content.ReadAsStringAsync();
				List<CourseDTO> courses = JsonConvert.DeserializeObject<List<CourseDTO>>(result);
				if (courses == null)
				{
					courses = new List<CourseDTO>();
				}
				ViewData["courses"] = courses;
			}


			return View();
		}
	}
}
