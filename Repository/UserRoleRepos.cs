using DynamicData.Data;
using DynamicData.Interface;
using DynamicData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DynamicData.Repository
{
    public class UserRoleRepos : IUserRoles
    {
        private readonly DatabaseContext _context;



        public UserRoleRepos(DatabaseContext dbContext)
        {
            _context = dbContext;

        }
        public async Task<UserRole> Add(UserRole userRole)
        {
            try
            {

                _context.UserRole.Add(userRole);
                await _context.SaveChangesAsync();
                return userRole;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> Delete(int userId)
        {
            try
            {
                List<UserRole> userRoles = _context.UserRole.Where(w => w.UserID == userId).ToList();
                _context.RemoveRange(userRoles);
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
