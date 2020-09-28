using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamicData.Models
{
    public class OrganizationLibrary
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int OrganizationID { get; set; }
        public Organization Organization { get; set; }

        public int LibraryID { get; set; }
        public Library Library { get; set; }
    }
}
