using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Concrete.Auth
{
    [Table("Role", Schema = "Auth")]
    public class Role : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }

    }
}
