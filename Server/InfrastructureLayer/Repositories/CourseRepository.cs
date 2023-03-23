using ApplicationLayer.IRepositories;
using CoreLayer.Entities;

namespace InfrastructureLayer.Repositories;

public class CourseRepository : ICourseRepository
{
    public Task<Course> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Course>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<int> AddAsync(Course entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateAsync(Course entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}