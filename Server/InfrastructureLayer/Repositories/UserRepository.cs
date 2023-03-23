using ApplicationLayer.IRepositories;
using CoreLayer.Entities;
using SqlKata.Execution;

namespace InfrastructureLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly QueryFactory _queryFactory;

        public UserRepository(QueryFactory queryFactory)
        {
            _queryFactory = queryFactory;
        }

        public Task<int> AddAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _queryFactory.Query(Table.BASEUSERS_TABLE).GetAsync<User>();
        }

        public Task<User> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}