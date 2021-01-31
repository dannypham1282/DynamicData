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
    public class FieldValueRepo : IFieldValue
    {
        private readonly DatabaseContext _context;
        private readonly ICommon _iCommon;
        private readonly IItem _iItem;
       


        public FieldValueRepo(DatabaseContext dbContext, ICommon iCommon, IItem iItem)
        {
            _context = dbContext;
            _iCommon = iCommon;
            _iItem = iItem;
           
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
            return await _context.FieldValue.Where(w => w.FieldID == ID).OrderBy(o => o.Field.Title).ToListAsync();
        }

        public async Task<bool> UpdateAllRelatedDropdownValue(Guid libraryGuid, string fieldName, string newValue)
        {
            try
            {
                _iCommon.SPNonQuery("spUpdateValueForRelatedDropdown '" + libraryGuid + "','" + fieldName + "', '" + newValue + "'");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateValueForDropdownWhenDeleted(Guid libraryGuid, int itemID)
        {
            try
            {
                _iCommon.SPNonQuery("spUpdateValueForDropdownWhenDeleted '" + libraryGuid + "','" + itemID + "'");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> CheckDuplicateFieldValue(int fieldID, string value)
        {
            try
            {
                var fieldValue = await _context.FieldValue.Where(w => w.FieldID == fieldID && w.Value.ToLower().Trim() == value.ToLower().Trim()).AsNoTracking().FirstOrDefaultAsync();
                if (fieldValue != null)
                    return true;
                else return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> CalculateFormularField(int ItemID, Guid libraryGuid, Guid? currentFieldGuid)
        {
            try
            {
                var formularFields = await _context.Field.Where(w => w.LibraryGuid == libraryGuid && w.Formular != null).AsNoTracking().ToListAsync();
                foreach (var formularField in formularFields)
                {
                    var fieldValue = await _context.FieldValue.Where(w => w.FieldID == formularField.ID && w.ItemID == ItemID).AsNoTracking().FirstOrDefaultAsync();
                    if (fieldValue == null) //If no value record is created then create one
                    {
                        var item = await _iItem.FindByID(ItemID);
                        var newFieldValue = new FieldValue();
                        newFieldValue.Item = item;
                        newFieldValue.FieldID = formularField.ID;
                        newFieldValue.ItemGuid = item.GUID;
                        newFieldValue.LibraryGuid = libraryGuid;
                        newFieldValue.Updated = DateTime.Now;
                        newFieldValue.Value = "";
                        await this.Add(newFieldValue);
                    }

                    string formular = formularField.Formular;
                    if (!string.IsNullOrEmpty(formular))
                    {
                        string function = formular.Substring(0, formular.IndexOf("()["));// seperator for function. Index from 0 to the function seperator to find function name
                        string formularDef = formular.Substring(formular.IndexOf("]([") + 3).TrimEnd(new char[] { ']', ')' });// seperator for formular detail. substring from this seperator to the end of the formular string to find formular definition
                                                                                                                              //if (currentFieldGuid == null || formularDef.IndexOf(currentFieldGuid.ToString()) > -1) // Check current field is part of the formular definition. If current field is part of it, trigger the formular calculation else do nothing
                                                                                                                              // {
                        string calculatedFieldGuid = formularDef.Replace("F_", "");
                        _iCommon.SPNonQuery("spUpdateCellValueByFormular " + ItemID + ",'" + function + "','" + calculatedFieldGuid + "', '" + formularField.GUID + "'");
                        // }
                    }
                    //string libraryGuid = "";
                }
                // spUpdateCellValueByFormular 94,'MonthYear','8D9E1500-21D2-4317-BA71-B57AF88ECCB6','86D07847-DF28-4F6C-AE0F-573C11C635B4'
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

}
