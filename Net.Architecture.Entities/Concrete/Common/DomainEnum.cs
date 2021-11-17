using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Concrete.Common
{
    //This table used for storing enums that used in project.
    //Example Usage:
    //Id:10-ParentId:null-Description:Gender-Status:true
    //Id:10001-ParentId:10-Description:Male-Status:true
    //Id:10002-ParentId:10-Description:Female-Status:true
    //We can get enums under gender by parentId
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
