using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamicData.Models
{
    public class DefaultField
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public int? FieldTypeID { get; set; }

        public FieldType FieldType { get; set; }
    }
}
