using DynamicData.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DynamicData.Interface
{
    public interface IUser : IDisposable
    {
        public Task<List<User>> UserCollection(bool isSysAdmin, int? ogrId);


        public Task<User> Add(User user);
        public Task<User> Update(User user);
        public Task<bool> Delete(Guid Guid);
        public Task<bool> Delete(int ID);
        public Task<User> Edit(User user);

        public Task<User> FindByID(int ID);
        public Task<User> FindByGUID(Guid Guid);

        public Task<bool> UpdatePassword(int userId, string password);
    }
}
