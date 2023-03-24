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
    public async Task<StudentCourse?> GetById(Guid id)
    {
        return await _queryFactory.Query(Table.STUDENTCOURSES_TABLE).Where(Table.STUDENTCOURSES_TABLE_COURSEID, "=", id).FirstOrDefaultAsync<StudentCourse>();
    }

    public async Task<IEnumerable<StudentCourse>> GetAll()
    {
        return await _queryFactory.Query(Table.STUDENTCOURSES_TABLE).GetAsync<StudentCourse>();
    }

    public async Task<IEnumerable<StudentCourse>> GetByStudentId(Guid studentId)
    {
        return await _queryFactory.Query(Table.STUDENTCOURSES_TABLE).Where(Table.STUDENTCOURSES_TABLE_STUDENTID, "=", studentId).GetAsync<StudentCourse>();
    }

    public Task<int> Add(StudentCourse entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> Update(StudentCourse entity)
    {
        throw new NotImplementedException();
    }

    public async Task<int> Delete(Guid id)
    {
        return await _queryFactory.Query(Table.STUDENTCOURSES_TABLE).Where(Table.STUDENTCOURSES_TABLE_COURSEID, "=", id).DeleteAsync();
    }
}