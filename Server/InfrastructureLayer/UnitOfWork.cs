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
            ITeacherRepository teacherRepository,
            IStudentRepository studentRepository)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _courseRepository = courseRepository;
            _studentCourseRepository = studentCourseRepository;
            _categoryRepository = categoryRepository;
            _teacherRepository = teacherRepository;
            _studentRepository = studentRepository;
        }
        public IAccountRepository _accountRepository { get; }
        public IUserRepository _userRepository { get; }
        public ICourseRepository _courseRepository { get; }
        public IStudentCourseRepository _studentCourseRepository { get; }
        public ICategoryRepository _categoryRepository { get; }
        public ITeacherRepository _teacherRepository { get; }

        public IStudentRepository _studentRepository { get; }
    }
}