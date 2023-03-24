﻿using ApplicationLayer.IRepositories;
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

    public Task<Teacher?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Teacher>> GetAll()
    {
        return await _queryFactory.Query(Table.TEACHER_TABLE).GetAsync<Teacher>();
    }

    public Task<int> Add(Teacher entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> Update(Teacher entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}