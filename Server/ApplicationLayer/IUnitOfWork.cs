using ApplicationLayer.IRepositories;

namespace ApplicationLayer
{
    public interface IUnitOfWork
    {
        public IAccountRepository _accountRepository { get; }
    }
}