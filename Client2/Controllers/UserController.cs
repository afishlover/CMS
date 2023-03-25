using Client2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Client2.Controllers;

public class UserController : Controller
{
    private readonly HttpClient _client;
    private readonly IConfiguration _configuration;

    public UserController(HttpClient client, IConfiguration configuration)
    {
        _client = client;
        _configuration = configuration;
    }

    // GET
    public async Task<IActionResult> Lists()
    {
        var response = await _client.PostAsync(_configuration["ApiUrl"] + "/User/GetBasicUserInfo", null);
        var content = response.Content.ReadAsStringAsync().Result;
        dynamic? data = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<UserDTO>>(content);

        return View(data);
    }

    [HttpGet]
    public IActionResult ChangePassword()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ChangePassword(ChangePasswordDTO model)
    {
        Console.WriteLine(model.ToString());
        if (!ModelState.IsValid)
        {
            return View(model);
        }


        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public IActionResult ForgotPassword(ChangePasswordDTO model)
    {
        Console.WriteLine(model.ToString());
        if (!ModelState.IsValid)
        {
            return View("ChangePassword", model);
        }

        return RedirectToAction("Index", "Home");
    }
}