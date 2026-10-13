using MVCPro.DAL.Data;
using MVCPro.DAL.Interfaces;
using MVCPro.DAL.Models;
using System.Linq;

namespace MVCPro.DAL.Repositories
{
    public class UserRepo : GenericRepo<User>, IUserRepo
    {
        public UserRepo(AppDbContext context) : base(context) { }

        public bool IsEmailUnique(string email, int? excludeId = null)
        {
            return !_dbSet.Any(u => u.Email == email && (!excludeId.HasValue || u.Id != excludeId.Value));
        }

        public User? GetByIdForUpdate(int id)
        {
            return _dbSet.FirstOrDefault(u => u.Id == id);
        }
    }
}
