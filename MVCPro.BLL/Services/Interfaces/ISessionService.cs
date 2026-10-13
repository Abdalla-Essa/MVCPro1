using MVCPro.BLL.Helpers;
using MVCPro.BLL.ViewModels;

namespace MVCPro.BLL.Services.Interfaces
{
    public interface ISessionService
    {
        PaginatedList<SessionVM> GetAllPaged(int pageIndex, int pageSize);
        SessionVM? GetById(int id);
        void Create(CreateSessionVM vm);
        void Update(UpdateSessionVM vm);
        void Delete(int id);
    }
}
