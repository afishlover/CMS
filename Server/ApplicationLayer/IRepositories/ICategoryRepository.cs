using CoreLayer.Entities;

namespace ApplicationLayer.IRepositories;

public interface ICategoryRepository : IGenericRepository<Category>
{
    public Task<Category?> GetRecursiveCategoryByIdAsync(Guid id);
}