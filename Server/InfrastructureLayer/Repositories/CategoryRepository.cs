using ApplicationLayer.IRepositories;
using CoreLayer.Entities;
using SqlKata.Execution;

namespace InfrastructureLayer.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly QueryFactory _queryFactory;
    public CategoryRepository(QueryFactory queryFactory)
    {
        _queryFactory = queryFactory;
    }
    public Task<Category?> GetByIdAsync(Guid id)
    {
        return _queryFactory.Query(Table.CATEGORIES_TABLE).Where(Table.CATEGORIES_TABLE_CATEGORYID, "=", id).FirstOrDefaultAsync<Category?>();
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _queryFactory.Query(Table.CATEGORIES_TABLE).GetAsync<Category>();
    }

    public Task<int> AddAsync(Category entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateAsync(Category entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Category?> GetRecursiveCategoryByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}