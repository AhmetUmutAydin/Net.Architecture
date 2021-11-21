using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Net.Architecture.Entities.BaseEntities;
using Net.Architecture.Entities.Concrete.Common;

namespace Net.Architecture.Entities.Concrete.Auth
{
    [Table("User", Schema = "Auth")]
    public class User : BaseEntity
    {
        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        public string Username { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [Column("Password_Salt")]
        public byte[] PasswordSalt { get; set; }
        [Required]
        [Column("Password_Hash")]
        public byte[] PasswordHash { get; set; }
        [Column(@"Picture_Id")]
        public long? PictureId { get; set; }

        [ForeignKey("PictureId")] public virtual File File { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }
        public virtual UserRefreshToken UserRefreshToken { get; set; }

    }
}
