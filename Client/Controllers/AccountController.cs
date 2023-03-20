using System.Text;
using Client.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Client.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly HttpClient _client;

        private readonly IConfiguration _configuration;

        public AccountController(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        [HttpGet]
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AccountDTO accountDTO)
        {
            if (ModelState.IsValid)
            {
                string strData = JsonConvert.SerializeObject(accountDTO);
                HttpContent content = new StringContent(strData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(CmsApiUrl, content);
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
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Wrong username or password");
            }

            return View(accountDTO);
        }


        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("JWT");
            HttpContext.Session.Remove("JWT");
            HttpContext.Session.Remove("isLoggedIn");
            return RedirectToAction("Login", "Account");
        }

    }
}