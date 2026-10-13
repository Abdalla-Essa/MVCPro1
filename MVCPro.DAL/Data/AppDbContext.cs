using Microsoft.EntityFrameworkCore;
using MVCPro.DAL.Models;
using MVCPro.DAL.Enums;
using System;

namespace MVCPro.DAL.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

       
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Session> Sessions => Set<Session>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Grade> Grades => Set<Grade>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Instructor)
                .WithMany() 
                .HasForeignKey(c => c.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Session>()
                .HasOne(s => s.Course)
                .WithMany(c => c.Sessions)
                .HasForeignKey(s => s.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.User)
                .WithMany(u => u.Grades)
                .HasForeignKey(g => g.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Course)
                .WithMany(c => c.Grades)
                .HasForeignKey(g => g.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

           
            modelBuilder.Entity<Grade>()
                .HasIndex(g => new { g.UserId, g.CourseId })
                .IsUnique();

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FName = "Ahmed", LName = "Ali", Email = "ahmed@example.com", Role = "Admin", JoinedAt = new DateTime(2025, 9, 1) },
                new User { Id = 2, FName = "Sara", LName = "Hassan", Email = "sara@example.com", Role = "Student", JoinedAt = new DateTime(2025, 9, 1) },
                new User { Id = 3, FName = "Omar", LName = "Mahmoud", Email = "omar@example.com", Role = "Student", JoinedAt = new DateTime(2025, 9, 1) },
                new User { Id = 4, FName = "Nour", LName = "Fathy", Email = "nour@example.com", Role = "Student", JoinedAt = new DateTime(2025, 9, 1) },
                new User { Id = 5, FName = "Mona", LName = "Sami", Email = "mona@example.com", Role = "Instructor", JoinedAt = new DateTime(2025, 9, 1) }

            );

            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Name = "Robots", Description = "Robots with AI", Category = Category.AI, InstructorId = 5, StartDate = new DateTime(2025, 10, 1), EndDate = new DateTime(2026, 1, 1) },
                new Course { Id = 2, Name = "Physics 101", Description = "Basic Physics", Category = Category.General, InstructorId = 5, StartDate = new DateTime(2025, 10, 1), EndDate = new DateTime(2026, 1, 1) },
                new Course { Id = 3, Name = "C# Programming", Description = "Learn C# basics", Category = Category.Programming, InstructorId = 5, StartDate = new DateTime(2025, 10, 1), EndDate = new DateTime(2026, 1, 1) },
                new Course { Id = 4, Name = "UI/UX Design", Description = "Design Principles", Category = Category.Design, InstructorId = 5, StartDate = new DateTime(2025, 10, 1), EndDate = new DateTime(2026, 1, 1) },
                new Course { Id = 5, Name = "Business Management", Description = "Basics of Management", Category = Category.Business, InstructorId = 5, StartDate = new DateTime(2025, 10, 1), EndDate = new DateTime(2026, 1, 1) }
            );

            modelBuilder.Entity<Session>().HasData(
                new Session { Id = 1, Title = "Robots Session 1", CourseId = 1, StartTime = new DateTime(2025, 10, 2, 9, 0, 0), EndTime = new DateTime(2025, 10, 2, 11, 0, 0), Location = "Room 101" },
                new Session { Id = 2, Title = "Physics Session 1", CourseId = 2, StartTime = new DateTime(2025, 10, 2, 9, 0, 0), EndTime = new DateTime(2025, 10, 2, 11, 0, 0), Location = "Room 102" },
                new Session { Id = 3, Title = "C# Session 1", CourseId = 3, StartTime = new DateTime(2025, 10, 2, 9, 0, 0), EndTime = new DateTime(2025, 10, 2, 11, 0, 0), Location = "Room 103" },
                new Session { Id = 4, Title = "UI/UX Session 1", CourseId = 4, StartTime = new DateTime(2025, 10, 2, 9, 0, 0), EndTime = new DateTime(2025, 10, 2, 11, 0, 0), Location = "Room 104" },
                new Session { Id = 5, Title = "Business Session 1", CourseId = 5, StartTime = new DateTime(2025, 10, 2, 9, 0, 0), EndTime = new DateTime(2025, 10, 2, 11, 0, 0), Location = "Room 105" }
            );

            modelBuilder.Entity<Grade>().HasData(
                new Grade { Id = 1, UserId = 2, CourseId = 1, Score = 95, Remarks = "Excellent" },
                new Grade { Id = 2, UserId = 2, CourseId = 2, Score = 88, Remarks = "Good" },
                new Grade { Id = 3, UserId = 3, CourseId = 3, Score = 92, Remarks = "Very Good" },
                new Grade { Id = 4, UserId = 4, CourseId = 1, Score = 85, Remarks = "Good" },
                new Grade { Id = 5, UserId = 4, CourseId = 4, Score = 90, Remarks = "Very Good" },
                new Grade { Id = 6, UserId = 3, CourseId = 5, Score = 78, Remarks = "Satisfactory" }
            );
        }
    }
}
