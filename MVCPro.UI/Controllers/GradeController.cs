using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCPro.BLL.Services.Interfaces;
using MVCPro.BLL.ViewModels;
using MVCPro.DAL.Enums;
using MVCPro.DAL.Interfaces;
using System;
using System.Linq;

public class GradeController : Controller
{
    private readonly IGradeService _gradeService;
    private readonly IUserRepo _userRepo;
    private readonly ICourseRepo _courseRepo;

    public GradeController(IGradeService gradeService, IUserRepo userRepo, ICourseRepo courseRepo)
    {
        _gradeService = gradeService;
        _userRepo = userRepo;
        _courseRepo = courseRepo;
    }

    public IActionResult GetAll(int pageIndex = 1, int pageSize = 5)
    {
        var grades = _gradeService.GetAllPaged(pageIndex, pageSize);
        ViewBag.PageSize = pageSize;
        return View(grades);
    }

    [HttpGet]
    public IActionResult Create()
    {
        LoadDropdowns();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CreateGradeVM vm)
    {
        if (!ModelState.IsValid)
        {
            LoadDropdowns();
            return View(vm);
        }

        try
        {
            _gradeService.Create(vm);
            return RedirectToAction(nameof(GetAll)); 
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError("", ex.Message);
            LoadDropdowns();
            return View(vm);
        }
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        var vm = _gradeService.GetForUpdate(id);
        if (vm == null)
            return NotFound();

        LoadDropdowns();
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(UpdateGradeVM vm)
    {
        if (!ModelState.IsValid)
        {
            LoadDropdowns();
            return View(vm);
        }

        try
        {
            _gradeService.Update(vm);
            return RedirectToAction(nameof(GetAll)); 
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError("", ex.Message);
            LoadDropdowns();
            return View(vm);
        }
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var g = _gradeService.GetById(id); 
        if (g == null) return NotFound();
        return View(g);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ConfirmDelete(int id)
    {
        _gradeService.Delete(id);
        return RedirectToAction(nameof(GetAll)); 
    }

    private void LoadDropdowns()
    {
        ViewBag.Users = _userRepo.GetAll()
            .Where(u => u.Role.ToLower() == "student")
            .Select(u => new SelectListItem(u.FullName, u.Id.ToString()))
            .ToList();

        ViewBag.Courses = _courseRepo.GetAll()
            .Select(c => new SelectListItem(c.Name, c.Id.ToString()))
            .ToList();

        ViewBag.Remarks = Enum.GetValues(typeof(GradeRemarks))
            .Cast<GradeRemarks>()
            .Select(r => new SelectListItem
            {
                Text = string.Join(" ", r.ToString().Split('_')), 
                Value = r.ToString()
            })
            .ToList();
    }
}
