using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DynamicData.Models
{
    public class Permission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [AllowNull]
        public int Create { get; set; }

        [AllowNull]
        public int Read { get; set; }
        [AllowNull]
        public int Update { get; set; }
        [AllowNull]
        public int Delete { get; set; }

        public Permission()
        {

        }
    }
}
