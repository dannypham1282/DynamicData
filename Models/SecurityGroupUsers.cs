using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamicData.Models
{
    public class SecurityGroupUsers
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public List<SecurityGroup> SecurityGroup { get; set; }
        public List<User> User { get; set; }

    }
}
