using System.IdentityModel.Tokens.Jwt;
using Api.Models.DTOs;
using ApplicationLayer;
using AutoMapper;
using CoreLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

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
            var studentCourses = await _unitOfWork._studentCourseRepository.GetAll();
            var courses = await _unitOfWork._courseRepository.GetAll();
            var categories = await _unitOfWork._categoryRepository.GetAll();
            var teachers = await _unitOfWork._teacherRepository.GetAll();
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTeacherCoursesByTeacherId([FromBody] Guid teacherId)
        {
            try
            {
                var courses = await _unitOfWork._courseRepository.GetAll();
                var teacherCourses = courses.Where(c => c.TeacherId.Equals(teacherId)).Select(_ => _mapper.Map<Course, TeacherCourseDTO>(_));
                if (!teacherCourses.Any())
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                return Ok(teacherCourses);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UnEnrollStudentCourseById(Guid courseId)
        {
            try
            {
                Request.Headers.TryGetValue("Authorization", out var values);
                var token = values.ToString();
                var tokens = token.Split(" ");


                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(tokens[1]);
                var accountId = jwtSecurityToken.Claims.First(claim => claim.Type == "Id").Value;
                var student = await _unitOfWork._accountRepository.GetById(new Guid(accountId));

                if (student == null)
                {
                    return NotFound("Account not recognized");
                }

                var studentCourse = await _unitOfWork._studentCourseRepository.GetById(courseId);

                if (studentCourse == null)
                {
                    return NotFound("Student was not enroll this course!");
                }

                await _unitOfWork._studentCourseRepository.Delete(courseId);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}