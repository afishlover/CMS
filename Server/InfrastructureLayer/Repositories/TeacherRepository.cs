using ApplicationLayer.IRepositories;
using CoreLayer.Entities;
using SqlKata.Execution;

namespace InfrastructureLayer.Repositories;

public class TeacherRepository : ITeacherRepository
{
    private readonly QueryFactory _queryFactory;

    public TeacherRepository(QueryFactory queryFactory)
    {
        _queryFactory = queryFactory;
    }

    public Task<Teacher?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Teacher>> GetAllAsync()
    {
        return await _queryFactory.Query(Table.TEACHER_TABLE).GetAsync<Teacher>();
    }

    public Task<int> AddAsync(Teacher entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateAsync(Teacher entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Teacher?> GetTeacherByUserIdAsync(Guid id)
    {
        return await _queryFactory.Query(Table.TEACHER_TABLE).Where(Table.TEACHER_TABLE_USERID, "=", id).FirstOrDefaultAsync<Teacher>();
    }
}