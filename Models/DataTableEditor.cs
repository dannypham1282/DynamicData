using System.Collections.Generic;

namespace DynamicData.Models
{
    public class DataTableEditor
    {
        public string label { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public List<DataTableColumnOption> options { get; set; }
        public string DisplayFormat { get; set; }
    }
}
