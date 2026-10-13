using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCPro.BLL.Services.Interfaces;
using MVCPro.BLL.ViewModels;
using MVCPro.DAL.Interfaces;
using System.Linq;

namespace MVCPro.UI.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly ICourseRepo _courseRepo;

        public SessionController(ISessionService sessionService, ICourseRepo courseRepo)
        {
            _sessionService = sessionService;
            _courseRepo = courseRepo;
        }

        public IActionResult GetAll(int pageIndex = 1, int pageSize = 5)
        {
            var sessions = _sessionService.GetAllPaged(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            return View(sessions);
        }

        [HttpGet]
        public IActionResult Create()
        {
            LoadDropdowns();
            return View(new CreateSessionVM());
        }

        [HttpPost]
        public IActionResult Create(CreateSessionVM vm)
        {
            if (!ModelState.IsValid)
            {
                LoadDropdowns();
                return View(vm);
            }

            _sessionService.Create(vm);
            return RedirectToAction(nameof(GetAll));
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var s = _sessionService.GetById(id);
            if (s == null) return NotFound();

            LoadDropdowns();
            return View(new UpdateSessionVM
            {
                Id = s.Id,
                Title = s.Title,
                StartTime = s.StartDate,  
                EndTime = s.EndDate,      
                Location = s.Location
            });

        }

        [HttpPost]
        public IActionResult Update(UpdateSessionVM vm)
        {
            if (!ModelState.IsValid)
            {
                LoadDropdowns();
                return View(vm);
            }

            _sessionService.Update(vm);
            return RedirectToAction(nameof(GetAll));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var s = _sessionService.GetById(id);
            if (s == null) return NotFound();
            return View(s);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            _sessionService.Delete(id);
            return RedirectToAction(nameof(GetAll));
        }

        private void LoadDropdowns()
        {
            ViewBag.Courses = _courseRepo.GetAll()
                .Select(c => new SelectListItem(c.Name, c.Id.ToString()))
                .ToList();
        }
    }
}
