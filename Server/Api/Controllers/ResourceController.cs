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
    public class ResourceController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IJwtHandler _jwtHandler;
        private readonly IUnitOfWork _unitOfWork;

        public ResourceController(IUnitOfWork unitOfWork, IMapper mapper, IJwtHandler jwtHandler)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtHandler = jwtHandler;
        }

        [HttpGet("{id}")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetResourceById(Guid id)
        {
            try
            {
                Request.Headers.TryGetValue("Authorization", out var values);
                var accountId = _jwtHandler.GetAccountIdFromJwt(values);
                var account = await _unitOfWork._accountRepository.GetByIdAsync(new Guid(accountId));

                if (account == null)
                {
                    return NotFound("User associated with this account is not found");
                }

                var resource = await _unitOfWork._resourceRepository.GetByIdAsync(id);
                if (resource == null)
                {
                    return NotFound("No resource found with this id");
                }
                return Ok(JsonConvert.SerializeObject(_mapper.Map<ResourceDTO>(resource), Formatting.Indented));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetResourcesByCourseId(Guid id)
        {
            try
            {
                Request.Headers.TryGetValue("Authorization", out var values);
                var accountId = _jwtHandler.GetAccountIdFromJwt(values);
                var account = await _unitOfWork._accountRepository.GetByIdAsync(new Guid(accountId));

                if (account == null)
                {
                    return NotFound("User associated with this account is not found");
                }

                var course = await _unitOfWork._courseRepository.GetByIdAsync(id);

                if (course == null)
                {
                    return NotFound("No course with this id found");
                }

                var resources = await _unitOfWork._resourceRepository.GetResourcesByCourseId(course.CourseId);

                if (resources.Count() == 0)
                {
                    return NoContent();
                }

                return Ok(JsonConvert.SerializeObject(resources.Select(_ => _mapper.Map<ResourceDTO>(_)), Formatting.Indented));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
