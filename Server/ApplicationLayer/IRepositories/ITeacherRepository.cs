using CoreLayer.Entities;

namespace ApplicationLayer.IRepositories;

public interface ITeacherRepository : IGenericRepository<Teacher>
{
    public Task<Teacher?> GetTeacherByUserIdAsync(Guid id);
}