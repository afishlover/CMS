using Api.Interfaces;
using Api.Models.DTOs;
using ApplicationLayer;
using AutoMapper;
using CoreLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CourseController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IJwtHandler _jwtHandler;
        private readonly IUnitOfWork _unitOfWork;

        public CourseController(IMapper mapper, IUnitOfWork unitOfWork, IJwtHandler jwtHandler)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _jwtHandler = jwtHandler;
        }

        [HttpPost("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetStudentCoursesByStudentId(Guid id)
        {
            Request.Headers.TryGetValue("Authorization", out var values);
            var accountId = _jwtHandler.GetAccountIdFromJwt(values);
            var account = await _unitOfWork._accountRepository.GetByIdAsync(new Guid(accountId));

            if (account == null)
            {
                return NotFound("Account not recognized");
            }

            try
            {
                var studentCourses = await _unitOfWork._studentCourseRepository.GetAllAsync();
                var courses = await _unitOfWork._courseRepository.GetAllAsync();
                var categories = await _unitOfWork._categoryRepository.GetAllAsync();
                var teachers = await _unitOfWork._teacherRepository.GetAllAsync();
                var result = studentCourses
                    .Where(sc => sc.StudentId.Equals(id))
                    .Join(courses, sc => sc.CourseId, c => c.CourseId, (sc, c) => new { sc, c })
                    .Join(categories, scj => scj.c.CategoryId, c => c.CategoryId, (scj, c) => new { result1 = scj, c })
                    .Join(teachers, result2 => result2.result1.c.TeacherId, t => t.TeacherId, (scjcj, t) => (scjcj, t))
                    .Select(_ =>
                        _mapper.Map<(Course, Teacher, StudentCourse), StudentCourseDTO>(
                            (_.scjcj.result1.c, _.t, _.scjcj.result1.sc)));
                return Ok(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCoursesByTeacherId(Guid teacherId)
        {
            try
            {
                Request.Headers.TryGetValue("Authorization", out var values);
                var accountId = _jwtHandler.GetAccountIdFromJwt(values);
                var account = await _unitOfWork._accountRepository.GetByIdAsync(new Guid(accountId));

                if (account == null)
                {
                    return NotFound("Account not recognized");
                }


                var teacher = await _unitOfWork._teacherRepository.GetByUserIdAsync(teacherId);
                if (teacher == null)
                {
                    return Forbid("Your account is lmao");
                }

                var courses = await _unitOfWork._courseRepository.GetAllAsync();
                var teacherCourses = courses.Where(c => c.TeacherId.Equals(teacher.TeacherId)).Select(_ => _mapper.Map<Course, TeacherCourseDTO>(_));
                if (!teacherCourses.Any())
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                return Ok(JsonConvert.SerializeObject(teacherCourses, Formatting.Indented));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UnEnrollStudentCourseById(Guid id)
        {
            try
            {
                Request.Headers.TryGetValue("Authorization", out var values);
                var accountId = _jwtHandler.GetAccountIdFromJwt(values);
                var student = await _unitOfWork._accountRepository.GetByIdAsync(new Guid(accountId));

                if (student == null)
                {
                    return NotFound("User associated with this account is not found");
                }

                var studentCourse = await _unitOfWork._studentCourseRepository.GetByIdAsync(id);

                if (studentCourse == null)
                {
                    return NotFound("Student was not enroll this course!");
                }

                await _unitOfWork._studentCourseRepository.DeleteAsync(id);
                return Ok("Unenroll from course");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EnrollStudentCourseById(Guid id)
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

                var studentCourse = await _unitOfWork._studentCourseRepository.GetByIdAsync(id);

                if (studentCourse != null)
                {
                    return Conflict("Student already enrolled this course!");
                }
                var user = await _unitOfWork._userRepository.GetByAccountIdAsync(account.AccountId);
                
                var student = await _unitOfWork._studentRepository.GetByUserIdAsync(user.UserId);

                StudentCourse _ = new()
                {
                    CourseId = id,
                    EnrollDate = DateTime.Now,
                    StudentId = student.StudentId
                };

                await _unitOfWork._studentCourseRepository.AddAsync(_);
                return Ok("You have been enrolled");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{name}/{code}")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCourseByNameOrCode(string? name = null, string? code = null)
        {
            var _name = name ?? string.Empty;
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

                var courses = await _unitOfWork._courseRepository.GetAllAsync();
                return Ok(JsonConvert.SerializeObject(courses
                    .Where(c => c.CourseName.ToLower().Contains(_name.ToLower()) || c.CourseCode.ToLower().Contains(_code.ToLower()))
                    .Select(_ => _mapper.Map<CourseDTO>(_)), Formatting.Indented)
                    );
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
            
        }


        [HttpGet("{id}")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCourseById(Guid id)
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
                    return NotFound("Course with this id is not exist");
                }

                var category = await _unitOfWork._categoryRepository.GetByIdAsync(course.CategoryId);
                var result = _mapper.Map<CourseDTO>((course, category));
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{id}")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCourseByCategoryId(Guid id)
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

                var courses = await _unitOfWork._courseRepository.GetByCategoryId(id);
                if (courses.Count() == 0)
                {
                    return NoContent();
                }

                var category = await _unitOfWork._categoryRepository.GetByIdAsync(id);

                if (category == null)
                {
                    return NotFound("Category with this id is not exist");
                }
                var result = courses.Select(_ => _mapper.Map<CourseDTO>((_, category)));
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseDTO course)
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

                if(teacher == null)
                {
                    return Forbid();
                }

                var newCourse = _mapper.Map<Course>(course);
                newCourse.CourseId = Guid.NewGuid();
                newCourse.TeacherId = teacher.TeacherId;
                newCourse.Since = DateTime.Now;
                newCourse.LastUpdate = DateTime.Now;
                newCourse.CreatorCode = teacher.TeacherCode;
                await _unitOfWork._courseRepository.AddAsync(newCourse);
                return Ok("Course create successfully");
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("{id}")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCourse(Guid id)
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

                var course = await _unitOfWork._courseRepository.GetByIdAsync(id);
                if (course == null)
                {
                    return NotFound("No course with this id");
                }
                await _unitOfWork._studentCourseRepository.DeleteAsync(id);
                await _unitOfWork._resourceRepository.DeleteByCourseIdAsync(id);
                await _unitOfWork._courseRepository.DeleteAsync(id);

                return Ok("Delete course successfully");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCourse([FromBody] UpdateCourseDTO updateCourseDTO)
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

                var course = await _unitOfWork._courseRepository.GetByIdAsync(updateCourseDTO.CourseId);
                if (course == null)
                {
                    return NotFound("No course with this id");
                }
                var updateCourse = _mapper.Map<Course>(updateCourseDTO);
                await _unitOfWork._courseRepository.UpdateAsync(updateCourse);

                return Ok("Update course successfully");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}