using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DynamicData.Models
{
    public class FieldLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [AllowNull]
        public Guid GUID { get; set; }

        [AllowNull]
        public DateTime DateTime { get; set; }

        [AllowNull]
        public Field Field { get; set; }

        [AllowNull]
        public Library Library { get; set; }

        [AllowNull]
        public User EditedBy { get; set; }

        [AllowNull]
        [MaxLength(5000)]
        string JSONData { get; set; }

        public FieldLog()
        {
            this.GUID = System.Guid.NewGuid();
        }
    }
}
