using MVCPro.BLL.Helpers;
using MVCPro.BLL.Services.Interfaces;
using MVCPro.BLL.ViewModels;
using MVCPro.DAL.Interfaces;
using MVCPro.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVCPro.BLL.Services
{
    public class GradeService : IGradeService
    {
        private readonly IUnitOfWork _uow;

        public GradeService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // Get all grades
        public IEnumerable<GradeVM> GetAll()
        {
            var grades = _uow.Grades.GetAllWithDetails();

            return grades.Select(g => new GradeVM
            {
                Id = g.Id,
                UserName = g.User?.FullName ?? "N/A",
                CourseName = g.Course?.Name ?? "N/A",
                Score = g.Score,
                Remarks = g.Remarks
            }).ToList();
        }

        // Get all grades with pagination
        public PaginatedList<GradeVM> GetAllPaged(int pageIndex, int pageSize)
        {
            var grades = _uow.Grades.GetAllWithDetails();

            var vm = grades.Select(g => new GradeVM
            {
                Id = g.Id,
                UserName = g.User?.FullName ?? "N/A",
                CourseName = g.Course?.Name ?? "N/A",
                Score = g.Score,
                Remarks = g.Remarks
            });

            return PaginatedList<GradeVM>.Create(vm.AsQueryable(), pageIndex, pageSize);
        }

        // Get grade by Id (for details or delete)
        public GradeVM? GetById(int id)
        {
            var g = _uow.Grades.GetByIdWithDetails(id);
            if (g == null) return null;

            return new GradeVM
            {
                Id = g.Id,
                UserName = g.User?.FullName ?? "N/A",
                CourseName = g.Course?.Name ?? "N/A",
                Score = g.Score,
                Remarks = g.Remarks
            };
        }

        // Get grade for update
        public UpdateGradeVM? GetForUpdate(int id)
        {
            var g = _uow.Grades.GetById(id);
            if (g == null) return null;

            return new UpdateGradeVM
            {
                Id = g.Id,
                UserId = g.UserId,
                CourseId = g.CourseId,
                Score = g.Score,
                Remarks = g.Remarks
            };
        }

        // Create new grade
        public void Create(CreateGradeVM vm)
        {
            var exists = _uow.Grades.GetAll()
                .Any(g => g.CourseId == vm.CourseId && g.UserId == vm.UserId);

            if (exists)
                throw new InvalidOperationException("This student already has a grade for this course.");

            var grade = new Grade
            {
                CourseId = vm.CourseId,
                UserId = vm.UserId,
                Score = vm.Score,
                Remarks = vm.Remarks
            };

            _uow.Grades.Add(grade);
            _uow.Complete();
        }

        // Update existing grade
        public void Update(UpdateGradeVM vm)
        {
            var exists = _uow.Grades.GetAll()
                .Any(g => g.CourseId == vm.CourseId
                       && g.UserId == vm.UserId
                       && g.Id != vm.Id); // exclude the current grade being updated

            if (exists)
                throw new InvalidOperationException("This student already has a grade for this course.");

            var grade = _uow.Grades.GetById(vm.Id);
            if (grade == null)
                throw new InvalidOperationException("Grade not found.");

            grade.CourseId = vm.CourseId;
            grade.UserId = vm.UserId;
            grade.Score = vm.Score;
            grade.Remarks = vm.Remarks;

            _uow.Grades.Update(grade);
            _uow.Complete();
        }

        // Delete grade
        public void Delete(int id)
        {
            _uow.Grades.Delete(id);
            _uow.Complete();
        }
    }
}
