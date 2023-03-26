using Api.Interfaces;
using Api.Models.DTOs;
using ApplicationLayer;
using AutoMapper;
using CoreLayer.Entities;
using CoreLayer.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtHandler _jwtHandler;

        public UserController(IMapper mapper, IUnitOfWork unitOfWork, IJwtHandler jwtHandler)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _jwtHandler = jwtHandler;
        }

        [HttpPost]
        // [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBasicUserInfo()
        {
            try
            {
                var accounts = await _unitOfWork._accountRepository.GetAllAsync();
                var users = await _unitOfWork._userRepository.GetAllAsync();
                var result = accounts.Join(users, acc => acc.AccountId, u => u.AccountId,
                    (account, user) =>
                        new UserDTO
                        {
                            AccountId = account.AccountId,
                            UserId = user.UserId,
                            Email = account.Email,
                            Name = user.GetFullName(),
                            Phone = user.Phone,
                            Role = Enum.GetName(typeof(Role), account.Role),
                            Status = Enum.GetName(typeof(Status), account.Status),
                        });
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDetailedUserProfile()
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

                if (Enum.GetName(account.Role).Equals("Student"))
                {
                    var identity = await _unitOfWork._studentRepository.GetByUserIdAsync(user.UserId);
                    return Ok(_mapper.Map<DetailedUserDTO>((identity, user)));
                }
                else
                {
                    var identity = await _unitOfWork._teacherRepository.GetByUserIdAsync(user.UserId);
                    return Ok(_mapper.Map<DetailedUserDTO>((identity, user)));
                }

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        //for user 
        //[HttpPost]
        //[Authorize(Roles = "Student, Teacher")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> GetUserInfoByAccountId(Guid id)
        //{
        //    var
        //}
    }
}