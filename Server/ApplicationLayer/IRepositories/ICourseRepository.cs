using CoreLayer.Entities;

namespace ApplicationLayer.IRepositories;

public interface ICourseRepository : IGenericRepository<Course>
{
    public Task<IEnumerable<Course>> GetByCategoryId(Guid id);
}