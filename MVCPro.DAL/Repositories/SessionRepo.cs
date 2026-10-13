using MVCPro.DAL.Data;
using MVCPro.DAL.Interfaces;
using MVCPro.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MVCPro.DAL.Repositories
{
    public class SessionRepo : GenericRepo<Session>, ISessionRepo
    {
        public SessionRepo(AppDbContext context) : base(context) { }

        public IEnumerable<Session> GetAllWithCourse()
        {
            return _context.Sessions.Include(s => s.Course).ToList();
        }

        public Session? GetByIdWithCourse(int id)
        {
            return _context.Sessions.Include(s => s.Course)
                                    .FirstOrDefault(s => s.Id == id);
        }
    }
}
