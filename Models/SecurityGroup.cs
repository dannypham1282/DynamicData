using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DynamicData.Models
{
    public class SecurityGroup
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Name { get; set; }

        [NotMapped]
        List<User> UserCollection { get; set; }

        [AllowNull]
        public Permission Permission { get; set; }

        List<SecurityGroupUsers> SecurityGroupUsers { get; set; }
        public SecurityGroup()
        {

        }

    }
}
