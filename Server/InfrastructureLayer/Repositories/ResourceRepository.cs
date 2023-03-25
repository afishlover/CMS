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

        public Task<int> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Resource>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Resource?> GetByIdAsync(Guid id)
        {
            return _queryFactory.Query(Table.RESOURCES_TABLE).Where(Table.RESOURCES_TABLE_RESOURCEID, "=", id).FirstOrDefaultAsync<Resource?>();
        }

        public Task<int> UpdateAsync(Resource entity)
        {
            throw new NotImplementedException();
        }
    }
}
