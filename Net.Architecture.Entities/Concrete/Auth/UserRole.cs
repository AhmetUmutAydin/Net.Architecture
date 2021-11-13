using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Concrete.Auth
{
    [Table("User_Role", Schema = "Auth")]
    public class UserRole : BaseEntity
    {
        [Required]
        [Column("User_Id")]
        public long UserId { get; set; }
        [Required]
        [Column("Role_Id")]
        public long RoleId { get; set; }
        [ForeignKey("UserId")] public virtual User User { get; set; }
        [ForeignKey("RoleId")] public virtual Role Role { get; set; }

    }
}
