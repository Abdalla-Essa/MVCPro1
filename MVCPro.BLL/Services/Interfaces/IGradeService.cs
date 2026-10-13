using MVCPro.BLL.Helpers;
using MVCPro.BLL.ViewModels;
using System.Collections.Generic;

namespace MVCPro.BLL.Services.Interfaces
{
    public interface IGradeService
    {
        IEnumerable<GradeVM> GetAll();

        PaginatedList<GradeVM> GetAllPaged(int pageIndex, int pageSize);

        GradeVM? GetById(int id);

        UpdateGradeVM? GetForUpdate(int id);

        void Create(CreateGradeVM vm);

        void Update(UpdateGradeVM vm);

        
        void Delete(int id);
    }
}
