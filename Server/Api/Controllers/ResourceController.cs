using Api.Interfaces;
using Api.Models.DTOs;
using ApplicationLayer;
using AutoMapper;
using CoreLayer.Entities;
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
        private readonly IFileHandler _fileHandler;

        public ResourceController(IUnitOfWork unitOfWork, IMapper mapper, IJwtHandler jwtHandler, IFileHandler fileHandler)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtHandler = jwtHandler;
            _fileHandler = fileHandler;
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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

        [HttpDelete("{id}")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteResource(Guid id)
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
                var user = await _unitOfWork._userRepository.GetByAccountIdAsync(account.AccountId);
                var teacher = await _unitOfWork._teacherRepository.GetByUserIdAsync(user.UserId);

                if (teacher == null)
                {
                    return Forbid("Your account is lmao");
                }

                var resource = await _unitOfWork._resourceRepository.GetByIdAsync(id);
                if (resource == null)
                {
                    return NotFound("No resource found with this id");
                }

                await _unitOfWork._resourceRepository.DeleteAsync(id);

                return Ok("Deleted resource");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost, Consumes("multipart/form-data")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateResource([FromBody] CreateResourceDTO createResourceDTO) //not implemented
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
                var user = await _unitOfWork._userRepository.GetByAccountIdAsync(account.AccountId);
                var teacher = await _unitOfWork._teacherRepository.GetByUserIdAsync(user.UserId);

                if (teacher == null)
                {
                    return Forbid();
                }

                var resource = _mapper.Map<Resource>(createResourceDTO);

                var course = await _unitOfWork._courseRepository.GetByIdAsync(resource.CourseId);
                if (course == null)
                {
                    return NotFound("No course with this id");
                }

                resource.ResourceId = Guid.NewGuid();
                resource.CreatorId = teacher.TeacherId;
                resource.Since = DateTime.Now;
                resource.LastUpdate = DateTime.Now;
                resource.Status = 0;
                resource.ResourceType = CoreLayer.Enums.ResourceType.Upload;

                //if (resource.ResourceType != 0)
                //{
                //    var formCollection = await Request.ReadFormAsync();
                //    var file = formCollection.Files[0];
                //    var folderPath = $"Resources/{course.CategoryId}/{course.CourseId}";
                //    var savePath = Path.Combine(Directory.GetCurrentDirectory(), folderPath);
                //    var fileName = $"{resource.ResourceId}";
                //    await _fileHandler.Upload(file, savePath, fileName);
                //    resource.FileURL = Path.Combine(savePath, fileName);
                //}
                await _unitOfWork._resourceRepository.AddAsync(resource);
                return Ok("Resource created");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteResourceById(Guid id)
        {
            try
            {
                Request.Headers.TryGetValue("Authorization", out var values);
                var accountId = _jwtHandler.GetAccountIdFromJwt(values);
                var account = await _unitOfWork._accountRepository.GetByIdAsync(new Guid(accountId));

        //        if (account == null)
        //        {
        //            return NotFound("User associated with this account is not found");
        //        }
        //        var user = await _unitOfWork._userRepository.GetByAccountIdAsync(account.AccountId);
        //        var teacher = await _unitOfWork._teacherRepository.GetByUserIdAsync(user.UserId);

                //if (teacher == null)
                //{
                //    return Forbid();
                //}
                var resource = await _unitOfWork._resourceRepository.GetByIdAsync(id);
                if (resource == null)
                {
                    return NotFound("No resource with this id found");
                }

                var course = await _unitOfWork._courseRepository.GetByIdAsync(resource.CourseId);
                if (resource.FileURL != null)
                {
                    var folderPath = $"Resources/{course.CategoryId}/{course.CourseId}";
                    var savePath = Path.Combine(Directory.GetCurrentDirectory(), folderPath);
                    await _fileHandler.Delete(savePath);
                }
                await _unitOfWork._resourceRepository.DeleteAsync(id);
                return Ok("Deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
