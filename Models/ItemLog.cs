using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DynamicData.Models
{
    public class ItemLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [AllowNull]
        public Guid GUID { get; set; }
        public Item Item { get; set; }
        public DateTime DateTime { get; set; }

        [AllowNull]
        public User EditedBy { get; set; }

        [AllowNull]
        string JSONData { get; set; }
        public ItemLog()
        {
            this.GUID = Guid.NewGuid();
        }
    }
}
