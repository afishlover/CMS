using CoreLayer.Entities;

namespace  ApplicationLayer.IRepositories {
    public interface IAccountRepository : IGenericRepository<Account> {
        public Task<Account?> GetAccountByEmailAndPasswordAsync(string email, string password);
    }
}