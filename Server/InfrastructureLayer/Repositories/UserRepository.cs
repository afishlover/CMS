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

        public Task<User?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _queryFactory.Query(Table.USERS_TABLE).GetAsync<User>();
        }

        public Task<int> AddAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetUserByAccountIdAsync(Guid id)
        {
            return await _queryFactory.Query(Table.USERS_TABLE).Where(Table.USERS_TABLE_ACCOUNTID, "=", id).FirstOrDefaultAsync<User>();
        }
    }
}