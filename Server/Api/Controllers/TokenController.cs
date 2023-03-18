using Api.Interfaces;
using Api.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers {

    [ApiController]
    [Route("[controller]")]
    public class TokenController : ControllerBase {
        private readonly IJwtHandler _jwtHandler;
        public TokenController(IJwtHandler jwtHandler)
        {
            _jwtHandler = jwtHandler;
        }

        [HttpPost("[action]")]
        public IActionResult Post(AccountDTO accountDTO) {
            if(ModelState.IsValid) {
                dynamic account = 0;
                if(account != null) {
                    try {
                        return Ok(_jwtHandler.GenerateJwtToken(account));
                    }
                    catch (Exception) {
                        return StatusCode(StatusCodes.Status500InternalServerError);
                    }
                }
            } 
            return BadRequest();
        }

    }
}