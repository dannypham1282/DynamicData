using DynamicData.Data;
using DynamicData.Interface;
using DynamicData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicData.Repository
{
    public class UserRepo : IUser
    {
        private readonly DatabaseContext _context;
        public UserRepo(DatabaseContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<User> Add(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(Guid Guid)
        {
            var user = FindByGUID(Guid);
            _context.Remove(user);
            await _context.SaveChangesAsync();
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(int ID)
        {
            var user = FindByID(ID);
            _context.Remove(user);
            await _context.SaveChangesAsync();
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            try
            {
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<User> Edit(User user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
            return user;
            throw new NotImplementedException();
        }

        public async Task<User> FindByGUID(Guid Guid)
        {
            return await _context.User.Where(w => w.GUID == Guid).FirstOrDefaultAsync();
            throw new NotImplementedException();
        }

        public async Task<User> FindByID(int ID)
        {
            return await _context.User.Where(w => w.ID == ID).FirstOrDefaultAsync();
            throw new NotImplementedException();
        }

        public async Task<List<User>> UserCollection()
        {
            return await _context.User.ToListAsync();
            throw new NotImplementedException();
        }
    }
}
