using DynamicData.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DynamicData.Interface
{
    public interface ILibraryType : IDisposable
    {
        public Task<List<LibraryType>> LibraryTypeCollection();

        public Task<LibraryType> FindByGUID(Guid Guid);
        public Task<LibraryType> FindByID(int ID);
        public Task<LibraryType> FindByName(string name);

        public Task<LibraryType> Add(LibraryType libraryType);
        public Task<bool> Delete(Guid Guid);
        public Task<bool> Delete(int ID);
        public Task<LibraryType> Edit(LibraryType libraryType);
    }
}
