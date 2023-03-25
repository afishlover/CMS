using CoreLayer.Entities;

namespace ApplicationLayer.IRepositories;

public interface IStudentCourseRepository : IGenericRepository<StudentCourse>
{
    public Task<IEnumerable<StudentCourse>> GetByStudentId(Guid studentId);
    public Task<IEnumerable<StudentCourse>> GetByCourseIdAsync(Guid courseId);
}