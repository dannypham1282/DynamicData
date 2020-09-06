using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DynamicData.Models
{
    public class ItemFile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [AllowNull]
        public Guid GUID { get; set; }

        [AllowNull]
        public Item Item { get; set; }
        public int ItemID { get; set; }

        [MaxLength(500)]
        [AllowNull]
        public string Filename { get; set; }

        [MaxLength(1000)]
        [AllowNull]
        public string FileLocation { get; set; }
        public ItemFile()
        {
            this.GUID = Guid.NewGuid();
        }
    }
}
