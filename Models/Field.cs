using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DynamicData.Models
{
    public class Field
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [AllowNull]
        public Guid GUID { get; set; }

        [MaxLength(500)]
        public string Name { get; set; }

        public string Title { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        [MaxLength]
        public string Description { get; set; }

        [AllowNull]
        public int? Editable { get; set; }

        [AllowNull]
        public int? Visible { get; set; }

        [AllowNull]
        [NotMapped]
        public Item Item { get; set; }

        [AllowNull]
        public Guid LibraryGuid { get; set; }
        [AllowNull]
        public Library Library { get; set; }

        [DisplayName("Field Type")]
        public int? FieldTypeID { get; set; }

        [AllowNull]

        [ForeignKey("FieldTypeID")]
        public FieldType FieldType { get; set; }

        [NotMapped]
        [AllowNull]
        public List<FieldType> FieldTypeCollection { get; set; }

        [NotMapped]
        [DisplayName("Field")]
        public List<Field> FieldCollection { get; set; }

        [DisplayName("Link to Library")]
        [AllowNull]
        public Guid? LookupTable { get; set; }

        [NotMapped]
        [AllowNull]
        public List<Library> LibraryCollection { get; set; }

        [NotMapped]
        public Guid? FormularLibraryGuid { get; set; }

        [AllowNull]
        public Guid? LookUpId { get; set; }
        [AllowNull]

        [DisplayName("Link Library Field Name")]

        public string LookUpValue { get; set; }

        [DisplayName("Dropdown/Radio Value (Seperate by ;)")]

        [AllowNull]
        public string DropdownValue { get; set; }

        [NotMapped]
        public List<DataTableColumnOption> options { get; set; }

        [AllowNull]
        public Library ActionButonOpenLibrary { get; set; }

        [AllowNull]
        public int? Deleted { get; set; }

        [NotMapped]
        [DisplayName("Visible")]
        public bool IsVisible { get; set; }
        [NotMapped]
        [DisplayName("Editable")]
        public bool IsEditable { get; set; }


        public int? Required { get; set; }

        [DisplayName("Required")]
        [NotMapped]
        public bool IsRequired { get; set; }
        [NotMapped]

        [DisplayName("Grouping")]
        public bool IsGrouping { get; set; }
        public int? Grouping { get; set; }
        public int? SortOrder { get; set; }

        [DisplayName("Default Sort")]
        [NotMapped]
        public bool IsDefaultSort { get; set; }
        public int? DefaultSort { get; set; }

        [DisplayName("Check Duplicate")]
        [NotMapped]
        public bool IsCheckDuplicate { get; set; }
        public int? CheckDubplicate { get; set; }

        [DisplayName("Sort Direction")]
        public string SortDirection { get; set; }

        [DisplayName("Value From Other Library")]
        public string ValueFromOtherLibrary { get; set; }

        public string Formular { get; set; }
        public string FormularView { get; set; }
        public Field()
        {
            this.GUID = System.Guid.NewGuid();

        }
    }
}
