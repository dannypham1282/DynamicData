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
    public class LibraryRepo : ILibrary
    {

        private readonly DatabaseContext _context;

        public LibraryRepo(DatabaseContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Library> Add(Library library)
        {
            try
            {
                await _context.Library.AddAsync(library);
                await _context.SaveChangesAsync();
                return library;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task<bool> CheckDuplicate(Library library)
        {
            if (_context.Library.Where(w => w.ID == library.ID).AsNoTracking().FirstOrDefault() != null) //it's ok to update it self
                return false;
            else
            {
                var checkLib = await _context.Library.Where(w => w.ParentID == library.ParentID && (w.Name == library.Name.Trim() || w.Title == library.Title.Trim())).AsNoTracking().FirstOrDefaultAsync();
                if (checkLib != null)
                    return true;
                else
                    return false;
            }
        }

        public async Task<bool> Delete(Guid Guid)
        {
            try
            {
                var library = await FindByGUID(Guid);
                _context.Remove(library);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Delete(int ID)
        {
            try
            {
                var library = await FindByID(ID);
                _context.Remove(library);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<Library> Edit(Library library)
        {
            try
            {
                _context.Library.Update(library);
                await _context.SaveChangesAsync();
                return library;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task<Library> FindByGUID(Guid Guid)
        {
            try
            {
                return await _context.Library.Where(w => w.GUID == Guid).AsNoTracking().FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task<Library> FindByID(int ID)
        {
            try
            {
                return await _context.Library.Where(w => w.ID == ID).AsNoTracking().FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task<Library> FindByName(string Name)
        {

            try
            {
                return await _context.Library.Where(w => w.Name.ToLower() == Name.Trim().ToLower()).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task<List<Library>> LibraryCollection()
        {
            return await _context.Library.Include(i => i.LibraryType).ToListAsync();
        }

        public async Task<List<Library>> LibraryCollectionForSideMenu()
        {
            try
            {

                return await _context.Library.Include(i => i.LibraryType).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task<bool> UpdateLibraryOrder(Guid guid, int sort)
        {
            try
            {
                var library = await _context.Library.Where(w => w.GUID == guid).AsNoTracking().FirstOrDefaultAsync();
                library.SortOrder = sort;
                _context.Update(library);
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
