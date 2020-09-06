using DynamicData.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DynamicData.Interface
{
    public interface IField
    {
        public Task<List<Field>> FieldCollection();
        public Task<List<Field>> FieldCollection(Guid libraryGuid);

        public Task<List<Field>> FieldCollectionForDatatable(Guid libraryGuid);

        public Task<Field> Add(Field field);

        public Task<Field> Delete(Guid Guid);
        public Task<bool> Delete(Field field);
        public Task<Field> Update(Field field);

        public Task<Field> Find(Guid Guid);
        public Task<Field> FindByGuid(Guid Guid);
        public Task<Field> FindByID(int ID);

        public Task<Field> FindByNameAndLibraryGuid(string name, Guid libraryGuid);

        public Task<bool> DeleteByLibrary(Guid Guid);

        public Task<Field> EmptyFieldForView();
    }
}
