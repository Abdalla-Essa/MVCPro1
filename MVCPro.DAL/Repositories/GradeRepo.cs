using Microsoft.EntityFrameworkCore;
using MVCPro.DAL.Data;
using MVCPro.DAL.Interfaces;
using MVCPro.DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace MVCPro.DAL.Repositories
{
    public class GradeRepo : GenericRepo<Grade>, IGradeRepo
    {

        public GradeRepo(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<Grade> GetAllWithDetails()
        {
            return _context.Grades
                .Include(g => g.User)
                .Include(g => g.Course)
                .ToList();
        }

        public Grade? GetByIdWithDetails(int id)
        {
            return _context.Grades
                .Include(g => g.User)
                .Include(g => g.Course)
                .FirstOrDefault(g => g.Id == id);
        }
    }
}
