using ApplicationLayer;
using ApplicationLayer.IRepositories;

namespace InfrastructureLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IAccountRepository accountRepository, IUserRepository userRepository)
        {
            _accountRepository = accountRepository;
            UserRepository = userRepository;
        }
        public IAccountRepository _accountRepository { get; }
        public IUserRepository UserRepository { get; }
    }
}