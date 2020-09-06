using DynamicData.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DynamicData.Interface
{
    public interface IDefaultField
    {
        public Task<List<DefaultField>> DefaultFieldCollection();

    }
}
