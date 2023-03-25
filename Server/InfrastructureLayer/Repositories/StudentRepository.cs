using ApplicationLayer.IRepositories;
using CoreLayer.Entities;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Repositories
{
    internal class StudentRepository : IStudentRepository
    {
        private readonly QueryFactory _queryFactory;

        public StudentRepository(QueryFactory queryFactory)
        {
            _queryFactory = queryFactory;
        }
        public Task<int> AddAsync(Student entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _queryFactory.Query(Table.STUDENT_TABLE).GetAsync<Student>();
        }

        public Task<Student?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Student?> GetByUserIdAsync(Guid id)
        {
            return await _queryFactory.Query(Table.STUDENT_TABLE).Where(Table.STUDENT_TABLE_USERID, "=", id).FirstOrDefaultAsync<Student?>();
        }

        public Task<int> UpdateAsync(Student entity)
        {
            throw new NotImplementedException();
        }
    }
}
