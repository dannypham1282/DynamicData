using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamicData.Models
{
    public class LinkLibrary
    {
        [Key]

        public int ID { get; set; }
        [NotMapped]
        public List<Field> Fields { get; set; }
        [NotMapped]

        [DisplayName("Field")]
        public Guid SetField { get; set; }
        [NotMapped]
        [DisplayName("Link To Library")]
        public List<Library> LinkToLibrary { get; set; }
        [NotMapped]
        [DisplayName("Dependent Field")]
        public List<Field> DependentFields { get; set; }

        [NotMapped]
        public Guid LibraryGuid { get; set; }
    }
}
