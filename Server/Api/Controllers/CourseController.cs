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
        public async Task<IActionResult> GetStudentCoursesByStudentId([FromBody] Guid? studentId)
        {
            var studentCourses = await _unitOfWork._studentCourseRepository.GetAllAsync();
            var courses = await _unitOfWork._courseRepository.GetAllAsync();
            var categories = await _unitOfWork._categoryRepository.GetAllAsync();
            var teachers = await _unitOfWork._teacherRepository.GetAllAsync();
            var result = studentCourses
                .Join(courses, sc => sc.CourseId, c => c.CourseId, (sc, c) => new {sc, c})
                .Join(categories, scj => scj.c.CategoryId, c => c.CategoryId, (scj, c) => new {scj, c})
                .Join(teachers, scjcj => scjcj.scj.c.TeacherId, t => t.TeacherId, (scjcj, t)  => new StudentCourseDTO
                {
                    CategoryId = scjcj.c.CategoryId,
                    CourseCode = scjcj.scj.c.CourseCode,
                    CourseName = scjcj.scj.c.CourseName,
                    EnrollDate = scjcj.scj.sc.EnrollDate != null ? scjcj.scj.sc.EnrollDate.Value.ToString("dd/MM/yyyy") : string.Empty,
                    TeacherCode = t.TeacherCode
                });
            return Ok(result);
        }
    }
}


