using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;
using Client.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Client.Controllers
{
	[AllowAnonymous]
	public class AccountController : Controller
	{
		private readonly HttpClient _client;

		private readonly IConfiguration _configuration;
		private string CmsApiUrl;

		public AccountController(HttpClient client, IConfiguration configuration)
		{
			_client = client;
			_configuration = configuration;
			CmsApiUrl = _configuration.GetSection("CmsApiRoot").Value;
		}

		[HttpGet]
		[Route("login")]
		public IActionResult Login()
		{
			var isLoggedIn = HttpContext.Session.GetString("isLoggedIn");
			if (isLoggedIn != null && isLoggedIn.Equals("true"))
			{
				return RedirectToAction("Index", "Home");
			}
			return View();
		}

		[HttpPost]
		[Route("login")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(AccountDTO accountDTO)
		{
			if (ModelState.IsValid)
			{
				string strData = JsonConvert.SerializeObject(accountDTO);
				HttpContent content = new StringContent(strData, Encoding.UTF8, "application/json");
				HttpResponseMessage response = await _client.PostAsync(CmsApiUrl + "/token/post", content);
				if (response.IsSuccessStatusCode)
				{
					// Get the token from response
					var token = await response.Content.ReadAsStringAsync();

					// Decode the token and get the role of account
					var handler = new JwtSecurityTokenHandler();
					var jwtSecurityToken = handler.ReadJwtToken(token.Replace('"', ' ').Trim());
					var role = jwtSecurityToken.Claims.First(claim => claim.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;
					var accountId = jwtSecurityToken.Claims.First(claim => claim.Type == "Id").Value;

					// Store data in session
					HttpContext.Session.SetString("Role", role.ToString());
					HttpContext.Session.SetString("AccountId", accountId.ToString());
					HttpContext.Session.SetString("JWT", token.Replace('"', ' ').Trim());
					HttpContext.Session.SetString("isLoggedIn", "true");

					//get User
					_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace('"', ' ').Trim());
					response = await _client.GetAsync(CmsApiUrl + "/user/GetDetailedUserProfile");
					if (response.IsSuccessStatusCode)
					{
						var result = await response.Content.ReadAsStringAsync();
						DetailedUserDTO user = JsonConvert.DeserializeObject<DetailedUserDTO>(result);
						if (user != null)
						{
							HttpContext.Session.SetString("FullName", user.FullName);
							HttpContext.Session.SetString("Email", user.StudentCode ?? user.TeacherCode);
						}
					}


					return RedirectToAction("Index", "Home");
				}

				ModelState.AddModelError(string.Empty, "Wrong username or password");
			}

			return View(accountDTO);
		}


		[HttpGet]
		[Route("/logout")]
		public IActionResult Logout()
		{
			HttpContext.Session.Remove("JWT");
			HttpContext.Session.Remove("JWT");
			HttpContext.Session.Remove("isLoggedIn");
			return RedirectToAction("Login", "Account");
		}


		[HttpGet]
		[Route("/me")]
		public IActionResult MyProfile()
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

		[HttpGet]
		[Route("/account/{id}")]
		public IActionResult UserProfile(string id)
		{
			HttpContext.Request.Headers.TryGetValue("Authorization", out var token);
			if (string.IsNullOrEmpty(token))
			{
				return RedirectToAction("Login", "Account");
			}
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());

			var role = HttpContext.Session.GetString("Role");
			ViewData["role"] = role;


			return View("MyProfile");
		}

		[HttpGet]
		[Route("/changepassword")]
		public IActionResult ChangePassword()
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
		[Route("/changepassword")]
		public async Task<IActionResult> ChangePassword(ChangePasswordDTO changePasswordDTO)
		{
			HttpContext.Request.Headers.TryGetValue("Authorization", out var token);
			if (string.IsNullOrEmpty(token))
			{
				return RedirectToAction("Login", "Account");
			}
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());

			var role = HttpContext.Session.GetString("Role");
			ViewData["role"] = role;

			if (ModelState.IsValid)
			{
				string strData = JsonConvert.SerializeObject(changePasswordDTO);
				HttpContent content = new StringContent(strData, Encoding.UTF8, "application/json");
				HttpResponseMessage response = await _client.PostAsync(CmsApiUrl + "/account/changepassword", content);
				if (response.IsSuccessStatusCode)
				{
					return RedirectToAction("Logout", "Account");
				}

				ModelState.AddModelError(string.Empty, "Invalid information");
			}
			return View(changePasswordDTO);
		}
	}
}