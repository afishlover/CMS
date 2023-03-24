using Api.Interfaces.IServices;
using Api.Models.DTOs;
using Api.Utils;
using Api.Utils.RandomPassword;
using ApplicationLayer;
using CoreLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISendMailService _sendMailService;


        public AccountController(IUnitOfWork unitOfWork, ISendMailService sendMailService)
        {
            _unitOfWork = unitOfWork;
            _sendMailService = sendMailService;
        }

        [HttpPost]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _unitOfWork._accountRepository.GetAllAsync();
                return Ok(users);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO resetPasswordDTO)
        {
            try
            {
                var account = await _unitOfWork._accountRepository.GetAccountByEmailAsync(resetPasswordDTO.Email);

                if (account == null)
                {
                    return NotFound("Account with this email not found");
                }

                var generator = new PasswordGenerator(minimumLengthPassword: 8,
                                          maximumLengthPassword: 15,
                                          minimumUpperCaseChars: 2,
                                          minimumNumericChars: 3,
                                          minimumSpecialChars: 2);
                string password = generator.Generate().ShuffleTextSecure();

                account.Password = password;

                await _unitOfWork._accountRepository.UpdateAsync(account);

                await _sendMailService.SendEmailAsync(account.Email, "RESET PASSWORD REQUEST"
                    , $"This is your auto-generate password {password}. Change it later in Setting.");
                return Ok("Your password has been reset. Check your inbox");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}