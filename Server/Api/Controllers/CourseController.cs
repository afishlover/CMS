using Api.Models.DTOs;
using ApplicationLayer;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers {

    [ApiController]
    [Route("[controller]/[action]")]
    public class CourseController : ControllerBase {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CourseController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> GetStudentCoursesByStudentId([FromBody] Guid studentId)
        {
            var studentCourses = await _unitOfWork._studentCourseRepository.GetByStudentIdAsync(studentId);
            var courses = await _unitOfWork._courseRepository.GetAllAsync();
            var categories = await _unitOfWork._categoryRepository.GetAllAsync();
            var result = studentCourses
                .Join(courses, sc => sc.CourseId, c => c.CourseId, (sc, c) => new {sc, c})
                .Join(categories, scj => scj.c.CategoryId, c => c.CategoryId, (scj, c) => new StudentCourseDTO
                {
                    CategoryId = c.CategoryId,
                    CourseCode = scj.c.CourseCode,
                    CourseName = scj.c.CourseName,
                    EnrollDate = scj.sc.EnrollDate
                });
            return Ok(result);
        }
    }
}


