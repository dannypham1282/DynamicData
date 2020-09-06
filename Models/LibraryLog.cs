using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DynamicData.Models
{
    public class LibraryLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [AllowNull]
        public Guid GUID { get; set; }

        [AllowNull]
        public DateTime EditedDate { get; set; }

        [AllowNull]
        public Library Library { get; set; }

        [AllowNull]
        public User EditedBy { get; set; }

        [AllowNull]
        [MaxLength(5000)]
        string JSONData { get; set; }

        public LibraryLog()
        {
            this.GUID = Guid.NewGuid();
        }

    }
}
