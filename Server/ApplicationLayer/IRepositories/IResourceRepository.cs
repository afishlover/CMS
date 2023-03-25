using CoreLayer.Entities;

namespace ApplicationLayer.IRepositories;

public interface IResourceRepository : IGenericRepository<Resource>
{
    public Task<IEnumerable<Resource>> GetResourcesByCourseId(Guid id);
}