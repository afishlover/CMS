using Api.Models.DTOs;
using ApplicationLayer;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        
        public UserController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> GetDetailedUserInformation()
        {
            var accounts = await _unitOfWork._accountRepository.GetAllAsync();
            var users = await _unitOfWork._baseUserRepository.GetAllAsync();
            var result = accounts.Join(users, account => account.AccountId, user => user.AccountId, (account, user) =>
                new UserDTO()
                {
                    AccountId = account.AccountId,
                    UserId = user.UserId,
                    Email = account.Email
                });
            return Ok(result);
        }
    }
}