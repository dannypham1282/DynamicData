using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DynamicData.Models
{
    public class LibraryType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [AllowNull]
        public string Type { get; set; }
        [AllowNull]
        public string Controller { get; set; }

        public string Icon { get; set; }

        public LibraryType()
        {

        }
    }
}
