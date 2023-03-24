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

        public Task<User?> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _queryFactory.Query(Table.USERS_TABLE).GetAsync<User>();
        }

        public Task<int> Add(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}