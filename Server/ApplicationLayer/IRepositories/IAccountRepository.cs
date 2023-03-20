using CoreLayer.Entities;

namespace  ApplicationLayer.IRepositories {
    public interface IAccountRepository : IGenericRepository<Account> {
        public Account? GetAccountByEmailAndPassword(string email, string password);
    }
}