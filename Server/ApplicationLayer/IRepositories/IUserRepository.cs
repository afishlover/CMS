using CoreLayer.Entities;

namespace ApplicationLayer.IRepositories {
    public interface IUserRepository : IGenericRepository<User> {
        public Task<User?> GetByAccountIdAsync(Guid id);
    }
}