using ApplicationLayer;
using ApplicationLayer.IRepositories;

namespace InfrastructureLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IAccountRepository accountRepository, IBaseUserRepository baseUserRepository)
        {
            _accountRepository = accountRepository;
            _baseUserRepository = baseUserRepository;
        }
        public IAccountRepository _accountRepository { get; }
        public IBaseUserRepository _baseUserRepository { get; }
    }
}