using ApplicationLayer;
using ApplicationLayer.IRepositories;

namespace InfrastructureLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IAccountRepository accountRepository, 
            IUserRepository userRepository, 
            ICourseRepository courseRepository,
            IStudentCourseRepository studentCourseRepository,
            ICategoryRepository categoryRepository,
            ITeacherRepository teacherRepository)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _courseRepository = courseRepository;
            _studentCourseRepository = studentCourseRepository;
            _categoryRepository = categoryRepository;
            _teacherRepository = teacherRepository;
        }
        public IAccountRepository _accountRepository { get; }
        public IUserRepository _userRepository { get; }
        public ICourseRepository _courseRepository { get; }
        public IStudentCourseRepository _studentCourseRepository { get; }
        public ICategoryRepository _categoryRepository { get; }
        public ITeacherRepository _teacherRepository { get; }
    }
}