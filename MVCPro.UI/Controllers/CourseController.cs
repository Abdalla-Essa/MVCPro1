using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCPro.BLL.Helpers;
using MVCPro.BLL.Services.Interfaces;
using MVCPro.BLL.ViewModels;
using MVCPro.DAL.Models;
using MVCPro.DAL.Enums;
using System;
using System.Linq;

namespace MVCPro.BLL.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IUserService _userService;

        public CourseController(ICourseService courseService, IUserService userService)
        {
            _courseService = courseService;
            _userService = userService;
        }

        public IActionResult GetAll(string? name, Category? category, int pageIndex = 1, int pageSize = 5)
        {
            var courses = _courseService.GetAllPaged(name, category, pageIndex, pageSize);

            ViewBag.AllCategories = Enum.GetValues(typeof(Category))
                .Cast<Category>()
                .Select(c => new SelectListItem
                {
                    Text = c.ToString(),
                    Value = c.ToString(),
                    Selected = (category != null && c == category)
                }).ToList();

            ViewBag.SearchName = name;
            ViewBag.CurrentPageSize = pageSize;

            return View(courses);
        }

        [HttpGet]
        public IActionResult Create()
        {
            LoadInstructors();
            LoadCategories();
            return View(new CreateCourseVM());
        }

        [HttpPost]
        public IActionResult Create(CreateCourseVM vm)
        {
            if (!ModelState.IsValid)
            {
                LoadInstructors(vm.InstructorId);
                LoadCategories(vm.Category);
                return View(vm);
            }

            _courseService.Create(vm);
            return RedirectToAction(nameof(GetAll));
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var vm = _courseService.GetCourseForUpdate(id);
            if (vm == null) return NotFound();

            LoadInstructors(vm.InstructorId);
            LoadCategories(vm.Category);
            return View(vm);
        }

        [HttpPost]
        public IActionResult Update(int id, UpdateCourseVM vm)
        {
            if (!ModelState.IsValid)
            {
                LoadInstructors(vm.InstructorId);
                LoadCategories(vm.Category);
                return View(vm);
            }

            _courseService.Update(vm);
            return RedirectToAction(nameof(GetAll));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var course = _courseService.GetCourseDetailsVM(id);
            if (course == null) return NotFound();

            return View(course);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _courseService.Delete(id);
            return RedirectToAction(nameof(GetAll));
        }


        [HttpGet]
        public IActionResult Details(int id)
        {
            var course = _courseService.GetCourseDetailsVM(id);
            if (course == null) return NotFound();

            return View(course); 
        }
        private void LoadInstructors(int? selectedInstructorId = null)
        {
            var instructors = _userService.GetAllInstructors()
                .Select(u => new SelectListItem
                {
                    Text = $"{u.FName} {u.LName}",
                    Value = u.Id.ToString(),
                    Selected = (selectedInstructorId.HasValue && u.Id == selectedInstructorId.Value)
                }).ToList();

            ViewBag.Instructors = instructors;
        }

        private void LoadCategories(Category? selectedCategory = null)
        {
            ViewBag.AllCategories = Enum.GetValues(typeof(Category))
                .Cast<Category>()
                .Select(c => new SelectListItem
                {
                    Text = c.ToString(),
                    Value = c.ToString(),
                    Selected = selectedCategory.HasValue && c == selectedCategory.Value
                }).ToList();
        }
    }
}
