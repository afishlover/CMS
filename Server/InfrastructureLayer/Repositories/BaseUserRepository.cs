using ApplicationLayer.IRepositories;
using CoreLayer.Entities;
using SqlKata.Execution;

namespace InfrastructureLayer.Repositories
{
    public class BaseUserRepository : IBaseUserRepository
    {
        private readonly QueryFactory _queryFactory;

        public BaseUserRepository(QueryFactory queryFactory)
        {
            _queryFactory = queryFactory;
        }

        public Task<int> AddAsync(BaseUser entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BaseUser>> GetAllAsync()
        {
            return await _queryFactory.Query(Table.BASEUSERS_TABLE).GetAsync<BaseUser>();
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