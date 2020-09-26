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
    public class OrganizationRepo : IOrganization
    {
        private readonly DatabaseContext _context;
        public OrganizationRepo(DatabaseContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Organization> Add(Organization organization)
        {
            _context.Organization.Add(organization);
            await _context.SaveChangesAsync();
            return organization;
        }

        public async Task<bool> AddUserOrganization(int userId, int orgId)
        {
            try
            {
                UserOrganization userOrganization = new UserOrganization();
                userOrganization.User.ID = userId;
                userOrganization.Organization.ID = orgId;
                _context.UserOrganization.Add(userOrganization);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Delete(int orgId)
        {
            try
            {
                var org = await FindByID(orgId);
                _context.Organization.Remove(org);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteOrganizationFromUser(int orgId)
        {
            try
            {
                var userOrganization = await _context.UserOrganization.Where(w => w.Organization.ID == orgId).ToListAsync();
                _context.UserOrganization.RemoveRange(userOrganization);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteUserFromOrganization(int userId)
        {
            try
            {
                var userOrganization = await _context.UserOrganization.Where(w => w.User.ID == userId).ToListAsync();
                _context.UserOrganization.RemoveRange(userOrganization);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Organization> FindByID(int orgId)
        {
            return await _context.Organization.Where(w => w.ID == orgId).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<Organization> FindByUserId(int userId)
        {
            var userOrganization = await _context.UserOrganization.Where(w => w.User.ID == userId).AsNoTracking().FirstOrDefaultAsync();
            var org = await _context.Organization.Where(w => w.ID == userOrganization.Organization.ID).AsNoTracking().FirstOrDefaultAsync();
            return org;
        }

        public async Task<List<User>> FindUserByOrganization(int orgId)
        {
            var users = await _context.User.Include(i => i.UserOrganization).ThenInclude(i => i.Organization.ID == orgId).ToListAsync();
            return users;
        }

        public async Task<List<Organization>> OrganizationCollection()
        {
            try
            {
                var organizations = await _context.Organization.ToListAsync();
                return organizations;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> Update(Organization organization)
        {
            try
            {
                _context.Organization.Update(organization);
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
