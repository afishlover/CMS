using Api.Interfaces;
using Api.Models.DTOs;
using ApplicationLayer;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public string GetHashed(string input) {
            return BCrypt.Net.BCrypt.HashPassword(input);
        }
        
        [HttpGet]
        public string GetGuid() {
            return Guid.NewGuid().ToString();
        }
    }
}