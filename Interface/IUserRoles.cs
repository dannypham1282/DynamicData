using DynamicData.Models;
using System.Threading.Tasks;

namespace DynamicData.Interface
{
    public interface IUserRoles
    {
        public Task<UserRole> Add(UserRole userRole);

        public Task<bool> Delete(int userId);

    }
}
