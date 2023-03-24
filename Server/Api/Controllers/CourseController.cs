using Api.Models.DTOs;
using ApplicationLayer;
using AutoMapper;
using CoreLayer.Entities;
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
                .Join(teachers, scjcj => scjcj.scj.c.TeacherId, t => t.TeacherId, (scjcj, t)  => (scjcj, t))
                // .Select(_ => new StudentCourseDTO
                // {
                //    CategoryId = _.scjcj.c.CategoryId,
                //    CourseCode = _.scjcj.scj.c.CourseCode,
                //    CourseName = _.scjcj.scj.c.CourseName,
                //    EnrollDate = _.scjcj.scj.sc.EnrollDate != null ? _.scjcj.scj.sc.EnrollDate.Value.ToString("dd/MM/yyyy") : string.Empty,
                //    TeacherCode = _.t.TeacherCode
                // })
                .Select(_ => _mapper.Map<(Course, Teacher, StudentCourse), StudentCourseDTO>((_.scjcj.scj.c, _.t, _.scjcj.scj.sc)));
            return Ok(result);
        }
    }
}

