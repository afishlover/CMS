using Api.Models.DTOs;
using ApplicationLayer;
using AutoMapper;
using CoreLayer.Entities;
using InfrastructureLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllRootCategories()
        {
            try
            {
                var result = await _unitOfWork._categoryRepository.GetAllAsync();
                return Ok(result.Where(r => r.Level == 0).Select(_ => _mapper.Map<RootCategoryDTO>(_)));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllChildCategoriesByParentId(Guid id)
        {
            try
            {
                var result = await _unitOfWork._categoryRepository.GetAllAsync();
                return Ok(result.Where(r => r.Level > 0 && r.ParentId != null).Select(_ => _mapper.Map<RootCategoryDTO>(_)));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategoryHierarchy()
        {
            var result = (await _unitOfWork._categoryRepository.GetAllAsync()).SelectMany(_ => _.)
        }

        async Task<IEnumerable<Category>> GetChild(Guid id)
        {
            var table = await _unitOfWork._categoryRepository.GetAllAsync();
            return table.Where(x => x.CategoryId == id || x.ParentId == id)
                        .Union(table.Where(x => x.ParentId == id)
                                    .SelectMany(y => GetChild(y.CategoryId)));
        }
    }
}

