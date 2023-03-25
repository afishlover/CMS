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
        return _queryFactory.Query(Table.COURSES_TABLE).Where(Table.COURSES_TABLE_COURSEID, "=", id).FirstOrDefaultAsync<Course?>();
    }

    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        return await _queryFactory.Query(Table.COURSES_TABLE).GetAsync<Course>();
    }

    public async Task<int> AddAsync(Course entity)
    {
        return await _queryFactory.Query(Table.COURSES_TABLE).InsertAsync(entity);
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