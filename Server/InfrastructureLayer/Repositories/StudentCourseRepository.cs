using ApplicationLayer.IRepositories;
using CoreLayer.Entities;
using SqlKata.Execution;

namespace InfrastructureLayer.Repositories;

public class StudentCourseRepository : IStudentCourseRepository
{
    private readonly QueryFactory _queryFactory;
    public StudentCourseRepository(QueryFactory queryFactory)
    {
        _queryFactory = queryFactory;
    }
    public Task<StudentCourse> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<StudentCourse>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<int> AddAsync(StudentCourse entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateAsync(StudentCourse entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<StudentCourse>> GetByStudentIdAsync(Guid studentId)
    {
        return await _queryFactory.Query(Table.STUDENTCOURSES_TABLE).Where(Table.STUDENTCOURSES_TABLE_STUDENTID, "=", studentId).GetAsync<StudentCourse>();
    }
}