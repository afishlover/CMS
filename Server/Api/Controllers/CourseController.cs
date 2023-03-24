using Api.Models.DTOs;
using ApplicationLayer;
using AutoMapper;
using CoreLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CourseController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CourseController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetStudentCoursesByStudentId([FromBody] Guid? studentId)
        {
            var studentCourses = await _unitOfWork._studentCourseRepository.GetAllAsync();
            var courses = await _unitOfWork._courseRepository.GetAllAsync();
            var categories = await _unitOfWork._categoryRepository.GetAllAsync();
            var teachers = await _unitOfWork._teacherRepository.GetAllAsync();
            var result = studentCourses
                .Where(sc => sc.StudentId.Equals(studentId))
                .Join(courses, sc => sc.CourseId, c => c.CourseId, (sc, c) => new { sc, c })
                .Join(categories, scj => scj.c.CategoryId, c => c.CategoryId, (scj, c) => new { result1 = scj, c })
                .Join(teachers, result2 => result2.result1.c.TeacherId, t => t.TeacherId, (scjcj, t) => (scjcj, t))
                .Select(_ =>
                    _mapper.Map<(Course, Teacher, StudentCourse), StudentCourseDTO>(
                        (_.scjcj.result1.c, _.t, _.scjcj.result1.sc)));
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTeacherCoursesByTeacherId([FromBody] Guid teacherId)
        {
            var courses = await _unitOfWork._courseRepository.GetAllAsync();
            var teacherCourses = courses.Where(c => c.TeacherId.Equals(teacherId)).Select(_ => _mapper.Map<Course, TeacherCourseDTO>(_));
            return Ok(teacherCourses);
        }
    }
}