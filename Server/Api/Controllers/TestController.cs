using Api.Interfaces;
using Api.Models.DTOs;
using ApplicationLayer;
using AutoMapper;
using CoreLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    public class TestController : ControllerBase
    {
        private readonly IMapper _mapper;

        public TestController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public string GetHashed(string input) {
            return BCrypt.Net.BCrypt.HashPassword(input);
        }
        
        [HttpGet]
        public string GetGuid() {
            return Guid.NewGuid().ToString();
        }

        [HttpGet]
        public IActionResult GetMapper()
        {
            return Ok(_mapper.Map<BaseUserDTO>(new BaseUser() { FirstName = "Trung", MiddleName = "Bui", LastName = "Duc"}));
        }
    }
}