using CoreLayer.Entities;

namespace ApplicationLayer.IRepositories;

public interface IStudentCourseRepository : IGenericRepository<StudentCourse>
{
    public Task<IEnumerable<StudentCourse>> GetByStudentIdAsync(Guid studentId);
}