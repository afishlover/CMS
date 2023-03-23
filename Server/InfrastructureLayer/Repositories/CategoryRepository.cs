using ApplicationLayer.IRepositories;
using CoreLayer.Entities;

namespace InfrastructureLayer.Repositories;

public class CategoryRepository : ICategoryRepository
{
    public Task<Category> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Category>> GetAllAsync()
    {
        throw new NotImplementedException();
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
}