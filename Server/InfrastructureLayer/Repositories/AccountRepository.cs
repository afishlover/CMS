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

        public Task<int> AddAsync(Account entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Account?> GetAccountByEmailAndPasswordAsync(string email, string password)
        {
            var account = await _queryFactory.Query(Table.ACCOUNTS_TABLE).Where(Table.ACCOUNTS_TABLE_EMAIL, "=", email).FirstOrDefaultAsync<Account>();
            if (account != null)
            {
                bool flag = BC.Verify(password, account.Password);
                return flag ? account : null;
            }
            return null;
        }

        public async Task<Account?> GetAccountByEmailAsync(string email)
        {
            return await _queryFactory.Query(Table.ACCOUNTS_TABLE).Where(Table.ACCOUNTS_TABLE_EMAIL, "=", email).FirstOrDefaultAsync<Account>();
        }

        public async Task<Account?> GetAccountByIdAsync(Guid accountId)
        {
            return await _queryFactory.Query(Table.ACCOUNTS_TABLE).Where(Table.ACCOUNTS_TABLE_ACCOUNTID, "=", accountId).FirstOrDefaultAsync<Account>();
        }
        
        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await _queryFactory.Query(Table.ACCOUNTS_TABLE).GetAsync<Account>();
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