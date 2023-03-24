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
    public Task<StudentCourse?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<StudentCourse>> GetAll()
    {
        return await _queryFactory.Query(Table.STUDENTCOURSES_TABLE).GetAsync<StudentCourse>();
    }

    public Task Add(StudentCourse entity)
    {
        throw new NotImplementedException();
    }

    public Task Update(StudentCourse entity)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid id)
    {
        _queryFactory.Query(Table.STUDENTCOURSES_TABLE).Where(Table.STUDENTCOURSES_TABLE_COURSEID, "=", id).AsDelete();
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<StudentCourse>> GetByStudentId(Guid studentId)
    {
        return await _queryFactory.Query(Table.STUDENTCOURSES_TABLE).Where(Table.STUDENTCOURSES_TABLE_STUDENTID, "=", studentId).GetAsync<StudentCourse>();
    }
}