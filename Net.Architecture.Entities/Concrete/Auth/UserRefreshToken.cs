using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Concrete.Auth
{
    [Table("User_Refresh_Token", Schema = "Auth")]
    public class UserRefreshToken : BaseEntity
    {
        [Column("User_Id")]
        [Required]
        public long UserId { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public DateTime Expiration { get; set; }
        [ForeignKey("UserId")] public virtual User User { get; set; }
    }
}
