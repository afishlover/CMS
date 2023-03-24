using CoreLayer.Entities;

namespace ApplicationLayer.IRepositories
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        public Task<Account?> GetAccountByEmailAndPassword(string email, string password);
        public Task<Account?> GetAccountByEmail(string email);
    }
}