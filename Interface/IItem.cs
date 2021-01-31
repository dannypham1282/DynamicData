using DynamicData.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DynamicData.Interface
{
    public interface IItem
    {
        public Task<List<Item>> ItemCollection();

        public Task<List<Item>> ItemCollection(Guid libraryGuid);

        public Task<Item> Add(Item item);

        public Task<Item> Delete(Guid Guid);

        public Task<Item> Delete(int id);
        public Task<Item> Update(Item item);
        public Task<Item> FindByGuid(Guid Guid);
        public Task<Item> FindByID(int ID);
    }
}
