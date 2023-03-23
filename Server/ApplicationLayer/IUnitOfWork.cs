using ApplicationLayer.IRepositories;

namespace ApplicationLayer
{
    public interface IUnitOfWork
    {
        public IAccountRepository _accountRepository { get; }
        public IUserRepository _userRepository { get; }
        public ICourseRepository _courseRepository { get; }
        public IStudentCourseRepository _studentCourseRepository { get; }
        public ICategoryRepository _categoryRepository { get; }
        public ITeacherRepository _teacherRepository { get; }
    }
}