using DynamicData.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DynamicData.Interface
{
    public interface ILibrary
    {
        public Task<List<Library>> LibraryCollection();

        public Task<Library> FindByGUID(Guid Guid);
        public Task<Library> FindByID(int ID);
        public Task<Library> FindByName(string Name);

        public Task<Library> Add(Library library);
        public Task<bool> Delete(Guid Guid);
        public Task<bool> Delete(int ID);
        public Task<Library> Edit(Library library);
        public Task<bool> CheckDuplicate(Library library);
        public Task<List<Library>> LibraryCollectionForSideMenu();

        public Task<bool> UpdateLibraryOrder(Guid guid, int sort);
    }
}
