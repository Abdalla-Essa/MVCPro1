using MVCPro.DAL.Data;
using MVCPro.DAL.Interfaces;

namespace MVCPro.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public ICourseRepo Courses { get; private set; }
        public ISessionRepo Sessions { get; private set; }
        public IUserRepo Users { get; private set; }
        public IGradeRepo Grades { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Courses = new CourseRepo(_context);
            Sessions = new SessionRepo(_context);
            Users = new UserRepo(_context);
            Grades = new GradeRepo(_context);
        }


        public int Complete() => _context.SaveChanges();
        public void Dispose() => _context.Dispose();
    }
}
