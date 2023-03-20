using ApplicationLayer.IRepositories;
using CoreLayer.Entities;

namespace InfrastructureLayer.Repositories {
    public class AccountRepository : IAccountRepository
    {
        public Task<int> AddAsync(Account entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Account? GetAccountByEmailAndPassword(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Account>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Account> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Account entity)
        {
            throw new NotImplementedException();
        }
    }
}