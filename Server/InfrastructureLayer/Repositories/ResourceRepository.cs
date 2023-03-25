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
    public class ResourceRepository : IResourceRepository
    {
        private readonly QueryFactory _queryFactory;
        public ResourceRepository(QueryFactory queryFactory)
        {
            _queryFactory = queryFactory;
        }

        public Task<int> AddAsync(Resource entity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            return await _queryFactory.Query(Table.RESOURCES_TABLE).Where(Table.RESOURCES_TABLE_RESOURCEID, "=", id).DeleteAsync();
        }

        public async Task<int> DeleteByCourseIdAsync(Guid id)
        {
            return await _queryFactory.Query(Table.RESOURCES_TABLE).Where(Table.RESOURCES_TABLE_COURSEID, "=", id).DeleteAsync();
        }

        public Task<IEnumerable<Resource>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Resource?> GetByIdAsync(Guid id)
        {
            return _queryFactory.Query(Table.RESOURCES_TABLE).Where(Table.RESOURCES_TABLE_RESOURCEID, "=", id).FirstOrDefaultAsync<Resource?>();
        }

        public async Task<IEnumerable<Resource>> GetResourcesByCourseId(Guid id)
        {
            return await _queryFactory.Query(Table.RESOURCES_TABLE).Where(Table.RESOURCES_TABLE_COURSEID, "=", id).GetAsync<Resource>();
        }

        public Task<int> UpdateAsync(Resource entity)
        {
            throw new NotImplementedException();
        }
    }
}
