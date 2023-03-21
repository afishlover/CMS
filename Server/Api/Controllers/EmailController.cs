using Api.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers {

    [AllowAnonymous]
    [ApiController]
    [Route("[controller]/[action]")]
    public class EmailController : ControllerBase {
        private readonly ISendMailService _sendMailService;

        public EmailController(ISendMailService sendMailService)
        {
            _sendMailService = sendMailService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SendEMailAsync(string email, string subject, string htmlMessage) {
            try {
                await _sendMailService.SendEmailAsync(email, subject, htmlMessage);
                return Ok("Your email has been sent.");
            }
            catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SendEMailsAsync(ICollection<string> email, string subject, string htmlMessage) {
            try {
                await _sendMailService.SendEmailsAsync(email, subject, htmlMessage);
                return Ok("Your emails has been sent.");
            }
            catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}