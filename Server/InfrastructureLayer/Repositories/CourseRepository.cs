using ApplicationLayer.IRepositories;
using CoreLayer.Entities;
using SqlKata.Execution;

namespace InfrastructureLayer.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly QueryFactory _queryFactory;

    public CourseRepository(QueryFactory queryFactory)
    {
        _queryFactory = queryFactory;
    }
    public Task<Course?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        return await _queryFactory.Query(Table.COURSES_TABLE).GetAsync<Course>();
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