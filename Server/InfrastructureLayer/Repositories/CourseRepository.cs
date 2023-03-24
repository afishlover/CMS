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
    public Task<Course?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Course>> GetAll()
    {
        return await _queryFactory.Query(Table.COURSES_TABLE).GetAsync<Course>();
    }

    public Task Add(Course entity)
    {
        throw new NotImplementedException();
    }

    public Task Update(Course entity)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}