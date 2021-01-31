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
    public class ItemRepo : IItem
    {

        private readonly DatabaseContext _context;

        public ItemRepo(DatabaseContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<Item> Add(Item item)
        {
            await _context.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Item> Delete(Guid Guid)
        {
            var item = await FindByGuid(Guid);
            _context.Item.Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Item> Delete(int id)
        {
            var fieldValue = await _context.FieldValue.Include(i => i.Item).Where(w => w.Item.ID == id).ToListAsync();
            _context.FieldValue.RemoveRange(fieldValue);
            await _context.SaveChangesAsync();

            var item = await FindByID(id);
            _context.Item.Remove(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<Item> FindByGuid(Guid Guid)
        {
            return await _context.Item.Where(w => w.GUID == Guid).FirstOrDefaultAsync();
        }

        public async Task<Item> FindByID(int ID)
        {
            return await _context.Item.Where(w => w.ID == ID).FirstOrDefaultAsync();
        }

        public async Task<List<Item>> ItemCollection()
        {
            return await _context.Item.ToListAsync();
        }

        public async Task<List<Item>> ItemCollection(Guid libraryGuid)
        {
            return await _context.Item.Where(w => w.LibraryGuid == libraryGuid).ToListAsync();
        }

        public async Task<Item> Update(Item item)
        {
            _context.Item.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }
    }
}
