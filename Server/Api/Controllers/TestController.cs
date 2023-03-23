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
        private readonly IUnitOfWork _unitOfWork;

        public TestController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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
        public async Task<IActionResult> TestRepo()
        {
            return Ok(await _unitOfWork._courseRepository.GetAllAsync());
        }
    }
}