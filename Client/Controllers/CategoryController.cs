using Client.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

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
			//Get current category details
			RootCategoryDTO current;
			HttpResponseMessage response = await _client.GetAsync(CmsApiUrl + "/category/GetCategoryById?id=" + id);
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
			response = await _client.GetAsync(CmsApiUrl + "/category/GetAllChildCategoriesByParentId");
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



			return View();
		}
	}
}
