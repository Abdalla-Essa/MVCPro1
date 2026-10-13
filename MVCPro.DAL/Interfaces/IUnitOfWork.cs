using MVCPro.DAL.Models;
using MVCPro.DAL.Repositories;
using System;

namespace MVCPro.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICourseRepo Courses { get; }
        ISessionRepo Sessions { get; }
        IUserRepo Users { get; }
        IGradeRepo Grades { get; }

        int Complete();
    }
}
