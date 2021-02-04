using DynamicData.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DynamicData.Interface
{
    public interface IFieldValue
    {
        public Task<List<FieldValue>> FieldValueCollection();

        public Task<FieldValue> Add(FieldValue fieldValue);

        public Task<bool> Delete(Guid Guid);
        public Task<bool> DeleteByLibrary(Guid libraryGuid);
        public Task<bool> DeleteByFieldAndLibrary(int fieldID, Guid libraryGuid);
        public Task<FieldValue> Update(FieldValue fieldValue);
        public Task<FieldValue> FindByGuid(Guid Guid);
        public Task<List<FieldValue>> FindByID(int ID);
        public Task<List<FieldValue>> FindByFieldID(int ID);
        public Task<FieldValue> FindByName(string name, Guid libraryGuid);
        public Task<List<FieldValue>> FindByLibrary(Guid Guid);

        //   public Task<FieldValue> FindByFieldAndLibrary(int fieldID, Guid libraryGuid);

        public Task<FieldValue> FindbyNameAndLibraryGuidAndItemID(string name, Guid libraryGuid, int itemID);
        public Task<bool> UpdateAllRelatedDropdownValue(Guid libraryGuid, string fieldName, string newValue,string currentValue);
        public Task<bool> UpdateValueForDropdownWhenDeleted(Guid libraryGuid, int itemID);

        public Task<bool> CheckDuplicateFieldValue(int fieldID, string value);

        public Task<bool> CalculateFormularField(int ItemID, Guid currentFieldGuid);

    }
}
