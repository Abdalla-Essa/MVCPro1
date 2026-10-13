using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCPro.BLL.Services.Interfaces;
using MVCPro.BLL.ViewModels;
using MVCPro.DAL.Enums;

namespace MVCPro.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult GetAll(string? search, int pageIndex = 1, int pageSize = 5)
        {
            var users = _userService.GetAllPaged(search, pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Search = search;
            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Roles = Enum.GetValues(typeof(UserRole))
                                .Cast<UserRole>()
                                .Select(r => new SelectListItem
                                {
                                    Text = r.ToString(),
                                    Value = r.ToString()
                                })
                                .ToList();

            return View(new CreateUserVM());
        }

        [HttpPost]
        public IActionResult Create(CreateUserVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            try
            {
                _userService.Create(vm);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Email", ex.Message);
                return View(vm);
            }

            return RedirectToAction(nameof(GetAll));
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var user = _userService.GetByIdForUpdate(id);
            if (user == null) return NotFound();

            ViewBag.Roles = Enum.GetValues(typeof(UserRole))
                                .Cast<UserRole>()
                                .Select(r => new SelectListItem
                                {
                                    Text = r.ToString(),
                                    Value = r.ToString(),
                                    Selected = r.ToString() == user.Role
                                })
                                .ToList();

            return View(user);
        }

        [HttpPost]
        public IActionResult Update(UpdateUserVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            try
            {
                _userService.Update(vm);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Email", ex.Message);
                return View(vm);
            }

            return RedirectToAction(nameof(GetAll));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var user = _userService.GetById(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            _userService.Delete(id);
            return RedirectToAction(nameof(GetAll));
        }
    }
}
