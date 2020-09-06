using DynamicData.Data;
using DynamicData.Interface;
using DynamicData.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DynamicData.Repository
{
    public class DefaultFieldRepo : IDefaultField
    {
        private readonly DatabaseContext _context;

        public DefaultFieldRepo(DatabaseContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<DefaultField>> DefaultFieldCollection()
        {
            return await _context.DefaultField.Include(i => i.FieldType).ToListAsync();
        }
    }
}
