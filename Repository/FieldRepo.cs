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
    public class FieldRepo : IField
    {
        private readonly DatabaseContext _context;
        private readonly IFieldValue _iFieldValue;


        public FieldRepo(DatabaseContext dbContext, IFieldValue iFieldValue)
        {
            _context = dbContext;
            _iFieldValue = iFieldValue;
        }

        public async Task<Field> Add(Field field)
        {
            try
            {
                await _context.AddAsync(field);
                await _context.SaveChangesAsync();
                return field;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<Field> Delete(Guid Guid)
        {
            var field = await FindByGuid(Guid);
            _context.Field.Remove(field);
            await _context.SaveChangesAsync();
            return field;
        }

        public async Task<bool> Delete(Field field)
        {
            _context.Field.Remove(field);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteByLibrary(Guid Guid)
        {
            try
            {

                var fields = await _context.Field.Where(w => w.LibraryGuid == Guid).AsNoTracking().ToListAsync();
                await _iFieldValue.DeleteByLibrary(Guid);
                if (fields.Count > 0)
                {
                    _context.RemoveRange(fields);
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Field> FindByGuid(Guid Guid)
        {
            var field = await _context.Field.Where(w => w.GUID == Guid).Include(i => i.FieldType).AsNoTracking().FirstOrDefaultAsync();
            var fieldTypeCollection = await _context.FieldType.ToListAsync();
            field.FieldTypeCollection = fieldTypeCollection;
            return field;
        }

        public async Task<List<Field>> FindByLibraryGuid(Guid libraryGuid)
        {
            return await _context.Field.Where(w => w.LibraryGuid == libraryGuid).AsNoTracking().ToListAsync();
        }

        public async Task<Field> Find(Guid Guid)
        {
            return await _context.Field.Where(w => w.GUID == Guid).AsNoTracking().FirstOrDefaultAsync();
        }


        public async Task<Field> FindByID(int ID)
        {
            return await _context.Field.Where(w => w.ID == ID).AsNoTracking().FirstOrDefaultAsync();
        }

        public Task<List<Field>> FieldCollection()
        {
            return _context.Field.Include(i => i.FieldType).OrderBy(o => o.SortOrder).ToListAsync();
        }

        public async Task<Field> Update(Field field)
        {
            var library = await _context.Library.Where(w => w.GUID == field.LibraryGuid).AsNoTracking().FirstOrDefaultAsync();
            field.Library = library;
            _context.Field.Update(field);
            await _context.SaveChangesAsync();
            return null;
        }


        public async Task<bool> UpdateSortOrder(Field field)
        {
            _context.Field.Update(field);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Field>> FieldCollection(Guid libraryGuid)
        {
            try
            {
                return await _context.Field.Include(i => i.FieldType).Where(w => w.LibraryGuid == libraryGuid && w.Visible == 1).OrderBy(o => o.SortOrder).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Field> FindByNameAndLibraryGuid(string name, Guid libraryGuid)
        {
            return await _context.Field.Include(i => i.FieldType).Where(w => w.Name.ToLower() == name.Trim().ToLower() && w.LibraryGuid == libraryGuid).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<Field> FindByNameAndLibraryGuidWithoutType(string name, Guid libraryGuid)
        {
            return await _context.Field.Where(w => w.Name.ToLower() == name.Trim().ToLower() && w.LibraryGuid == libraryGuid).AsNoTracking().FirstOrDefaultAsync();
        }


        public async Task<List<Field>> FieldCollectionForDatatable(Guid libraryGuid)
        {

            return await _context.Field.Include(i => i.FieldType).Where(w => w.LibraryGuid == libraryGuid && w.Visible == 1).ToListAsync();
        }

        public async Task<Field> EmptyFieldForView()
        {
            var fieldTypeCollection = await _context.FieldType.ToListAsync();
            var libraryCollection = await _context.Library.ToListAsync();


            Field field = new Field();
            field.FieldTypeCollection = fieldTypeCollection;
            field.LibraryCollection = libraryCollection;

            return field;
        }

    }
}
