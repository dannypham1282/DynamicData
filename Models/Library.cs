using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DynamicData.Models
{
    public class Library
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [AllowNull]

        public Guid GUID { get; set; }

        [MaxLength(500)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Title { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string Description { get; set; }

        [AllowNull]
        public DateTime? CreatedDate { get; set; }

        [AllowNull]
        public DateTime? EditedDate { get; set; }

        [AllowNull]
        public string GroupBy { get; set; }

        [AllowNull]
        public string OrderBy { get; set; }

        [AllowNull]
        public int? LibraryTypeID { get; set; }
        public LibraryType LibraryType { get; set; }

        [NotMapped]
        [AllowNull]
        public List<LibraryType> LibraryTypeCollection { get; set; }

        //[AllowNull]
        //public Library Parent { get; set; }
        [AllowNull]
        public int? ParentID { get; set; }

        [NotMapped]
        [AllowNull]
        public List<Library> LibraryCollection { get; set; }

        [AllowNull]
        public User CreatedBy { get; set; }

        [AllowNull]
        public User EditedBy { get; set; }
        public int? Deleted { get; set; }

        public int? Visible { get; set; }

        public string URL { get; set; }

        public string MenuType { get; set; }
        public int? SortOrder { get; set; }

        public Library()
        {
            this.Visible = 1;
            this.Deleted = 0;
            this.GUID = Guid.NewGuid();
        }
    }
}
