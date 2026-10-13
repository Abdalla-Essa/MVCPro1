using System;
using System.Collections.Generic;
using System.Linq;
using MVCPro.BLL.Helpers;
using MVCPro.BLL.Services.Interfaces;
using MVCPro.BLL.ViewModels;
using MVCPro.DAL.Interfaces;
using MVCPro.DAL.Models;
using MVCPro.DAL.Enums;

namespace MVCPro.BLL.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserRepo _userRepo;

        public CourseService(IUnitOfWork uow, IUserRepo userRepo)
        {
            _uow = uow;
            _userRepo = userRepo;
        }

        public List<User> GetAllInstructorsForDropdown()
        {
            return _uow.Users.GetAll()
                .Where(u => u.Role == "Instructor")
                .ToList();
        }
        public PaginatedList<CourseVM> GetAllPaged(string? name, Category? category, int pageIndex, int pageSize)
        {
            var courses = _uow.Courses.GetAll().ToList(); // IEnumerable

            if (!string.IsNullOrWhiteSpace(name))
                courses = courses.Where(c => !string.IsNullOrEmpty(c.Name) && c.Name.Contains(name)).ToList();

            if (category.HasValue)
                courses = courses.Where(c => c.Category == category.Value).ToList();

            var vm = courses.Select(c =>
            {
                var instructor = _userRepo.GetById(c.InstructorId);
                return new CourseVM
                {
                    Id = c.Id,
                    Name = c.Name ?? "",
                    Description = c.Description ?? "",
                    Category = c.Category,
                    InstructorName = instructor != null ? instructor.FName + " " + instructor.LName : "",
                    StartDate = c.StartDate ?? DateTime.MinValue,
                    EndDate = c.EndDate ?? DateTime.MinValue
                };
            }).AsQueryable();

            return PaginatedList<CourseVM>.Create(vm, pageIndex, pageSize);
        }

      

        public CourseDetailsVM GetCourseDetailsVM(int id)
        {
            var c = _uow.Courses.GetById(id) ?? throw new Exception("Course not found");
            var instructor = _userRepo.GetById(c.InstructorId);

            return new CourseDetailsVM
            {
                Id = c.Id,
                Name = c.Name ?? "",
                Description = c.Description ?? "",
                Category = c.Category,
                InstructorId = c.InstructorId,
                InstructorName = instructor != null ? instructor.FName + " " + instructor.LName : "",
                StartDate = c.StartDate ?? DateTime.MinValue,
                EndDate = c.EndDate ?? DateTime.MinValue
            };
        }

        public UpdateCourseVM GetCourseForUpdate(int id)
        {
            var c = _uow.Courses.GetById(id) ?? throw new Exception("Course not found");

            return new UpdateCourseVM
            {
                Id = c.Id,
                Name = c.Name ?? "",
                Description = c.Description ?? "",
                Category = c.Category,
                InstructorId = c.InstructorId,
                StartDate = c.StartDate ?? DateTime.MinValue,
                EndDate = c.EndDate ?? DateTime.MinValue
            };
        }

        public void Create(CreateCourseVM vm)
        {
            var c = new Course
            {
                Name = vm.Name,
                Description = vm.Description,
                Category = vm.Category ?? default(Category),
                InstructorId = vm.InstructorId,
                StartDate = vm.StartDate,
                EndDate = vm.EndDate
            };

            _uow.Courses.Add(c);
            _uow.Complete();
        }

        public void Update(UpdateCourseVM vm)
        {
            var c = _uow.Courses.GetById(vm.Id) ?? throw new Exception("Course not found");

            c.Name = vm.Name;
            c.Description = vm.Description;
            c.Category = vm.Category ?? default(Category);
            c.InstructorId = vm.InstructorId;
            c.StartDate = vm.StartDate;
            c.EndDate = vm.EndDate;

            _uow.Courses.Update(c);
            _uow.Complete();
        }

        public void Delete(int id)
        {
            _uow.Courses.Delete(id);
            _uow.Complete();
        }

        public bool IsCourseNameExists(string name, int? excludeId = null)
        {
            return !_uow.Courses.GetAll()
                .Any(c => c.Name == name && (!excludeId.HasValue || c.Id != excludeId.Value));
        }
    }
}
