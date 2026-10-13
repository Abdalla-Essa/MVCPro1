using MVCPro.BLL.Helpers;
using MVCPro.BLL.ViewModels;
using MVCPro.DAL.Models;

namespace MVCPro.BLL.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAllInstructors();

        IEnumerable<UserVM> GetAll(); // <-- مفقودة
        PaginatedList<UserVM> GetAllPaged(string? search, int pageIndex, int pageSize);
        UserVM? GetById(int id);
        UpdateUserVM? GetByIdForUpdate(int id);
        void Create(CreateUserVM vm);
        void Update(UpdateUserVM vm);
        void Delete(int id);
        bool IsEmailExists(string email, int? excludeId = null);

    }

}
