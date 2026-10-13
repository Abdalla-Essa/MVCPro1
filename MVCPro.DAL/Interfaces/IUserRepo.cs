using MVCPro.DAL.Models;

namespace MVCPro.DAL.Interfaces
{
    public interface IUserRepo : IGenericRepo<User>
    {
        bool IsEmailUnique(string email, int? excludeId = null);
    }
}
