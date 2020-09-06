using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DynamicData.Models
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [AllowNull]
        public Guid GUID { get; set; }

        [AllowNull]
        public Guid LibraryGuid { get; set; }

        [AllowNull]
        public List<Field> Fields { get; set; }

        [AllowNull]
        public int Deleted { get; set; }
        public Item()
        {
            this.Deleted = 0;
            this.GUID = Guid.NewGuid();
        }

    }
}
