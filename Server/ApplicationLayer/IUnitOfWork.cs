using ApplicationLayer.IRepositories;

namespace ApplicationLayer
{
    public interface IUnitOfWork
    {
        public IAccountRepository _accountRepository { get; }
        public IBaseUserRepository _baseUserRepository { get; }
    }
}