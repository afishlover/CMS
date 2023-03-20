using ApplicationLayer;
using ApplicationLayer.IRepositories;

namespace InfrastructureLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public IAccountRepository _accountRepository { get; }
    }
}