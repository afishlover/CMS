using Api.Interfaces;
using Api.Interfaces.IServices;
using Api.Models.DTOs;
using ApplicationLayer;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers {

    [ApiController]
    [Route("[controller]")]
    public class TokenController : ControllerBase {
        private readonly IJwtHandler _jwtHandler;
        private readonly ISendMailService _iSendMailService;
        private readonly IUnitOfWork _unitOfWork;
        public TokenController(IJwtHandler jwtHandler, IUnitOfWork unitOfWork, ISendMailService iSendMailService)
        {
            _jwtHandler = jwtHandler;
            _unitOfWork = unitOfWork;
            _iSendMailService = iSendMailService;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(AccountDTO accountDTO) {
            if(ModelState.IsValid) {
                var account = await _unitOfWork._accountRepository.GetAccountByEmailAndPasswordAsync(accountDTO.Email, accountDTO.Password);
                if(account != null) {
                    try {
                        if(Enum.GetName(account.Status).Equals("Inactive"))
                        {
                            return BadRequest("Your account is not activate");

                        }
                        return Ok(_jwtHandler.GenerateJwtToken(account));
                    }
                    catch (Exception) {
                        return StatusCode(StatusCodes.Status500InternalServerError);
                    }
                }
            } 
            return BadRequest("Invalid credentials");
        }
    }
}