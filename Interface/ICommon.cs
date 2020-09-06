using DynamicData.Models;
using System;
using System.Collections.Generic;

namespace DynamicData.Interface
{
    public interface ICommon
    {
        public List<Dictionary<string, object>> SPReader(string storeProcedure);

        public string BreadCrumbs(Guid Guid);
        public List<Library> LibraryHierarchy();
    }
}
