using ApplicationLayer.IRepositories;
using CoreLayer.Entities;

namespace InfrastructureLayer.Repositories {
    public class BaseUserRepository : IBaseUserRepository
    {
        public Task<int> AddAsync(BaseUser entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BaseUser>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BaseUser> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(BaseUser entity)
        {
            throw new NotImplementedException();
        }
    }
}