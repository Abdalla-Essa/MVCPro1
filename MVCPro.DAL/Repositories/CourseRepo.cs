using Microsoft.EntityFrameworkCore;
using MVCPro.DAL.Data;
using MVCPro.DAL.Interfaces;
using MVCPro.DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace MVCPro.DAL.Repositories
{
    public class CourseRepo : GenericRepo<Course>, ICourseRepo
    {
        public CourseRepo(AppDbContext context) : base(context) { }

        public bool IsCourseNameUnique(string? name, int? excludeId = null)
        {
            return !_context.Courses.Any(c =>
                c.Name == name &&
                (!excludeId.HasValue || c.Id != excludeId.Value));
        }

        public Course? GetCourseWithInstructorById(int id)
        {
            return _context.Courses
                .Include(c => c.Instructor)
                .FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Course> GetAllWithInstructor()
        {
            return _context.Courses.Include(c => c.Instructor).ToList();
        }
    }

}
