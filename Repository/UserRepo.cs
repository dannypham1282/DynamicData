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
        private readonly IUserRoles _iuserRoles;

        public UserRepo(DatabaseContext dbContext, IUserRoles iUserRoles)
        {
            _context = dbContext;
            _iuserRoles = iUserRoles;
        }
        public async Task<User> Add(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Update(User user)
        {
            try
            {
                _context.User.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
            return user;
        }

        public async Task<bool> Delete(Guid Guid)
        {
            var user = FindByGUID(Guid);
            _context.Remove(user);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> Delete(int ID)
        {
            try
            {
                await _iuserRoles.Delete(ID);
                var user = await FindByID(ID);
                _context.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
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

        }

        public async Task<User> FindByGUID(Guid Guid)
        {
            return await _context.User.Where(w => w.GUID == Guid).AsNoTracking().FirstOrDefaultAsync();

        }

        public async Task<User> FindByID(int ID)
        {
            return await _context.User.Where(w => w.ID == ID).AsNoTracking().FirstOrDefaultAsync();

        }

        public async Task<List<User>> UserCollection(bool isSysAdmin, int? orgId)
        {
            if (isSysAdmin)
                return await _context.User.Include(i => i.UserRole).ThenInclude(i => i.Role).ToListAsync();
            else
                return await _context.User.Include(i => i.UserRole).ThenInclude(i => i.Role).Include(i => i.UserOrganization).ThenInclude(i => i.Organization).Where(w => w.UserOrganization.Any(a => a.OrganizationID == orgId)).ToListAsync();


        }

        public async Task<bool> UpdatePassword(int userId, string password)
        {
            try
            {
                var user = await FindByID(userId);
                user.Password = HashPassword.DoHash(password);
                _context.User.Update(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
