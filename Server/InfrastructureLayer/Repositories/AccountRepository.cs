using ApplicationLayer.IRepositories;
using CoreLayer.Entities;
using SqlKata;
using BC = BCrypt.Net.BCrypt;
using SqlKata.Execution;

namespace InfrastructureLayer.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly QueryFactory _queryFactory;
        public AccountRepository(QueryFactory queryFactory)
        {
            _queryFactory = queryFactory;
        }

        public async Task<Account?> GetAccountByEmailAndPassword(string email, string password)
        {
            var account = await _queryFactory.Query(Table.ACCOUNTS_TABLE).Where(Table.ACCOUNTS_TABLE_EMAIL, "=", email).FirstOrDefaultAsync<Account>();
            if (account != null)
            {
                bool flag = BC.Verify(password, account.Password);
                return flag ? account : null;
            }
            return null;
        }

        public async Task<Account?> GetAccountByEmail(string email)
        {
            return await _queryFactory.Query(Table.ACCOUNTS_TABLE).Where(Table.ACCOUNTS_TABLE_EMAIL, "=", email).FirstOrDefaultAsync<Account>();
        }

        public async Task<Account?> GetById(Guid id)
        {
            return await _queryFactory.Query(Table.ACCOUNTS_TABLE).Where(Table.ACCOUNTS_TABLE_ACCOUNTID, "=", id).FirstOrDefaultAsync<Account>();
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            return await _queryFactory.Query(Table.ACCOUNTS_TABLE).GetAsync<Account>();
        }

        public Task Add(Account entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(Account entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}