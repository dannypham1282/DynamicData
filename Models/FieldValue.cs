using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DynamicData.Models
{
    public class FieldValue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [AllowNull]


        public Guid GUID { get; set; }

        public int? FieldID { get; set; }

        [AllowNull]
        [ForeignKey("FieldID")]
        public Field Field { get; set; }
        [AllowNull]
        public string Value { get; set; }
        [AllowNull]
        public Guid LibraryGuid { get; set; }
        [AllowNull]
        public DateTime Created { get; set; }
        [AllowNull]
        public DateTime Updated { get; set; }

        [AllowNull]
        public Guid ItemGuid { get; set; }
        public int ItemID { get; set; }
        public Item Item { get; set; }

        public FieldValue()
        {
            this.GUID = Guid.NewGuid();
        }
    }
}
