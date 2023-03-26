using Api.Interfaces;
using Api.Models.DTOs;
using Api.Utils;
using ApplicationLayer;
using AutoMapper;
using CoreLayer.Entities;
using InfrastructureLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJwtHandler _jwtHandler;

        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper, IJwtHandler jwtHandler)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtHandler = jwtHandler;
        }

        [HttpGet]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllRootCategories()
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

                var result = await _unitOfWork._categoryRepository.GetAllAsync();
                return Ok(result.Where(r => r.Level == 0).Select(_ => _mapper.Map<RootCategoryDTO>(_)));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllChildCategoriesByParentId(Guid id)
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

                var result = await _unitOfWork._categoryRepository.GetAllAsync();
                return Ok(result.Where(r => r.Level > 0 && r.ParentId != null && r.ParentId.Equals(id)).Select(_ => _mapper.Map<RootCategoryDTO>(_)));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategoryId(Guid id)
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

                var category = await _unitOfWork._categoryRepository.GetByIdAsync(id);

                if(category == null)
                {
                    return NotFound("No category with this id");
                }
                return Ok(_mapper.Map<RootCategoryDTO>(category));
                //return Ok(JsonConvert.SerializeObject(_mapper.Map<RootCategoryDTO>(category)));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCategories()
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

                var categories = await _unitOfWork._categoryRepository.GetAllAsync();

                if (!categories.Any())
                {
                    return NoContent();
                }
                return Ok(JsonConvert.SerializeObject(categories.Select(_ => _mapper.Map<RootCategoryDTO>(_))));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> GetCategoryHierarchy()
        //{
        //    var result = (await _unitOfWork._categoryRepository.GetAllAsync()).SelectMany(_ => _.)
        //}

        //async Task<IEnumerable<Category>> GetChild(Guid id)
        //{
        //    var table = await _unitOfWork._categoryRepository.GetAllAsync();
        //    return table.Where(x => x.CategoryId == id || x.ParentId == id)
        //                .Union(table.Where(x => x.ParentId == id).SelectMany(y => GetChild(y.CategoryId)));
        //}
    }
}

