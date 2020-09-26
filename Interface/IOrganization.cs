using DynamicData.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DynamicData.Interface
{
    public interface IOrganization
    {
        public Task<List<Organization>> OrganizationCollection();
        public Task<Organization> Add(Organization organization);
        public Task<bool> Update(Organization organization);
        public Task<bool> Delete(int orgId);
        public Task<Organization> FindByID(int orgId);

        public Task<Organization> FindByUserId(int userId);

        public Task<bool> AddUserOrganization(int userId, int orgId);

        public Task<List<User>> FindUserByOrganization(int orgId);

        public Task<bool> DeleteUserFromOrganization(int userId);

        public Task<bool> DeleteOrganizationFromUser(int orgId);

    }
}
