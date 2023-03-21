using ApplicationLayer;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers {

    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase {
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}