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
    public async Task<StudentCourse?> GetByIdAsync(Guid id)
    {
        return await _queryFactory.Query(Table.STUDENTCOURSES_TABLE).Where(Table.STUDENTCOURSES_TABLE_COURSEID, "=", id).FirstOrDefaultAsync<StudentCourse>();
    }

    public async Task<IEnumerable<StudentCourse>> GetAllAsync()
    {
        return await _queryFactory.Query(Table.STUDENTCOURSES_TABLE).GetAsync<StudentCourse>();
    }

    public async Task<IEnumerable<StudentCourse>> GetByStudentId(Guid studentId)
    {
        return await _queryFactory.Query(Table.STUDENTCOURSES_TABLE).Where(Table.STUDENTCOURSES_TABLE_STUDENTID, "=", studentId).GetAsync<StudentCourse>();
    }

    public async Task<int> AddAsync(StudentCourse entity)
    {
        return await _queryFactory.Query(Table.STUDENTCOURSES_TABLE).InsertAsync(entity);
    }

    public Task<int> UpdateAsync(StudentCourse entity)
    {
        throw new NotImplementedException();
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        return await _queryFactory.Query(Table.STUDENTCOURSES_TABLE).Where(Table.STUDENTCOURSES_TABLE_COURSEID, "=", id).DeleteAsync();
    }

    public async Task<IEnumerable<StudentCourse>> GetByCourseIdAsync(Guid courseId)
    {
        return await _queryFactory.Query(Table.STUDENTCOURSES_TABLE).Where(Table.STUDENTCOURSES_TABLE_COURSEID, "=", courseId).GetAsync<StudentCourse>();

    }
}