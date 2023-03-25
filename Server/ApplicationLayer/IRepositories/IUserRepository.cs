using CoreLayer.Entities;

namespace ApplicationLayer.IRepositories {
    public interface IUserRepository : IGenericRepository<User> {
        public Task<User?> GeByAccountIdAsync(Guid id);
    }
}