﻿using DynamicData.Data;
using DynamicData.Interface;
using DynamicData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicData.Repository
{
    public class FieldValueRepo : IFieldValue
    {
        private readonly DatabaseContext _context;

        public FieldValueRepo(DatabaseContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<FieldValue> Add(FieldValue fieldValue)
        {
            try
            {
                await _context.FieldValue.AddAsync(fieldValue);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
            return fieldValue;
        }

        public async Task<bool> Delete(Guid Guid)
        {
            var fieldValue = FindByGuid(Guid);
            _context.Remove(fieldValue);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteByFieldAndLibrary(int fieldID, Guid libraryGuid)
        {
            var fieldValue = await _context.FieldValue.Where(w => w.Field.ID == fieldID && w.LibraryGuid == libraryGuid).ToListAsync();
            _context.FieldValue.RemoveRange(fieldValue);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteByLibrary(Guid libraryGuid)
        {
            try
            {
                var fieldValue = await _context.FieldValue.Where(w => w.LibraryGuid == libraryGuid).ToListAsync();
                _context.FieldValue.RemoveRange(fieldValue);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<List<FieldValue>> FieldValueCollection()
        {
            return await _context.FieldValue.ToListAsync();
        }



        public async Task<FieldValue> FindByGuid(Guid Guid)
        {
            return await _context.FieldValue.Where(w => w.GUID == Guid).FirstOrDefaultAsync();
        }

        public async Task<List<FieldValue>> FindByID(int ID)
        {
            return await _context.FieldValue.Where(w => w.ID == ID).AsNoTracking().ToListAsync();
        }

        public async Task<List<FieldValue>> FindByLibrary(Guid libraryGuid)
        {
            return await _context.FieldValue.Where(w => w.LibraryGuid == libraryGuid).ToListAsync();
        }

        public async Task<FieldValue> FindByName(string name, Guid libraryGuid)
        {
            return await _context.FieldValue.Include(i => i.Field).Where(w => w.Field.Name.ToLower() == name.Trim().ToLower() && w.LibraryGuid == libraryGuid).FirstOrDefaultAsync();
        }

        public async Task<FieldValue> FindbyNameAndLibraryGuidAndItemID(string name, Guid libraryGuid, int itemID)
        {
            return await _context.FieldValue.Include(i => i.Item).Include(f => f.Field).ThenInclude(t => t.FieldType).Where(w => w.Field.Name.ToLower() == name.Trim().ToLower() && w.LibraryGuid == libraryGuid && w.Item.ID == itemID).FirstOrDefaultAsync();
        }

        public async Task<FieldValue> Update(FieldValue fieldValue)
        {
            _context.FieldValue.Update(fieldValue);
            await _context.SaveChangesAsync();
            return fieldValue;
        }

        public string DynamicFieldValue(Guid libraryGuid)
        {
            _context.Database.ExecuteSqlRaw("spLoadDynamicData " + libraryGuid.ToString());
            return "";
        }

        public async Task<List<FieldValue>> FindByFieldID(int ID)
        {
            return await _context.FieldValue.Where(w => w.FieldID == ID).ToListAsync();
        }
    }
}
