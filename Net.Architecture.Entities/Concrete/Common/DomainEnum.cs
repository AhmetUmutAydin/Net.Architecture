using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Concrete.Common
{
    [Table("Domain_Enum", Schema = "Common")]
    public class DomainEnum : BaseEntityNone
    {
        [Column("Parent_Id")]
        public long? ParentId { get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; }
        [ForeignKey("ParentId")] public virtual DomainEnum Parent { get; set; }
        public virtual ICollection<PersonalInformation> PersonalInformation { get; set; }
        public virtual ICollection<Communication> Communication { get; set; }
    }
}
