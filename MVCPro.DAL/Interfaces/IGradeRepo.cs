using MVCPro.DAL.Models;
using System.Collections.Generic;

namespace MVCPro.DAL.Interfaces
{
    public interface IGradeRepo : IGenericRepo<Grade>
    {
        IEnumerable<Grade> GetAllWithDetails();
        Grade? GetByIdWithDetails(int id);
    }
}
