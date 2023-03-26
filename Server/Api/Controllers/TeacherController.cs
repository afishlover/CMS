using Api.Interfaces;
using Api.Models.DTOs;
using ApplicationLayer;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJwtHandler _jwtHandler;
        public TeacherController(IUnitOfWork unitOfWork, IMapper mapper, IJwtHandler jwtHandler)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtHandler = jwtHandler;
        }

        [HttpGet("{code}")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetTeacherByTeacherCode(string? code = null)
        {
            var _code = code ?? string.Empty;
            try
            {
                Request.Headers.TryGetValue("Authorization", out var values);
                var accountId = _jwtHandler.GetAccountIdFromJwt(values);
                var account = await _unitOfWork._accountRepository.GetByIdAsync(new Guid(accountId));

                if (account == null)
                {
                    return NotFound("User associated with this account is not found");
                }

                var user = await _unitOfWork._userRepository.GeByAccountIdAsync(account.AccountId);
                var teachers = await _unitOfWork._teacherRepository.GetAllAsync();

                if(!teachers.Any())
                {
                    return NoContent();
                }
                return Ok(JsonConvert.SerializeObject(teachers
                    .Where(t => t.TeacherCode.ToLower().Contains(_code.ToLower()))
                    .Select(_ => _mapper.Map<TeacherDTO>((_, user)))
                    ,Formatting.Indented));

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

    }
}
