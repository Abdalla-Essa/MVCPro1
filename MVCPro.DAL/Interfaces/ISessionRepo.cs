using MVCPro.DAL.Models;
using System.Collections.Generic;

namespace MVCPro.DAL.Interfaces
{
    public interface ISessionRepo : IGenericRepo<Session>
    {
        IEnumerable<Session> GetAllWithCourse();
        Session? GetByIdWithCourse(int id);
    }
}
