using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DynamicData.Models
{
    public class UserRole
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [AllowNull]
        public int UserID { get; set; }
        public User User { get; set; }
        [AllowNull]
        public int RoleID { get; set; }
        public Role Role { get; set; }
        public UserRole()
        {

        }
    }
}
