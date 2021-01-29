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
    public class LibraryTypeRepo : ILibraryType
    {
        private readonly DatabaseContext _context;

        public LibraryTypeRepo(DatabaseContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<LibraryType> Add(LibraryType libraryType)
        {
            await _context.AddAsync(libraryType);
            await _context.SaveChangesAsync();
            throw new NotImplementedException();
        }


        public async Task<bool> Delete(int ID)
        {
            var libraryType = FindByID(ID);
            _context.Remove(libraryType);
            await _context.SaveChangesAsync();
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid Guid)
        {
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

        public async Task<LibraryType> Edit(LibraryType libraryType)
        {
            _context.Update(libraryType);
            await _context.SaveChangesAsync();
            return libraryType;
            throw new NotImplementedException();
        }

        public Task<LibraryType> FindByGUID(Guid Guid)
        {
            throw new NotImplementedException();
        }

        public async Task<LibraryType> FindByID(int ID)
        {
            return await _context.LibraryType.Where(w => w.ID == ID).FirstOrDefaultAsync();

        }

        public async Task<List<LibraryType>> LibraryTypeCollection()
        {
            return await _context.LibraryType.ToListAsync();

        }

        public async Task<LibraryType> FindByName(string name)
        {
            return await _context.LibraryType.Where(w => w.Type.ToLower() == name.ToLower().Trim()).AsNoTracking().FirstOrDefaultAsync();
        }

    }
}
