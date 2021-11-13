using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Concrete.Common
{
    [Table("Communication", Schema = "Common")]
    public class Communication : Auditable
    {
        [Required]
        [Column(@"Contact_Id")]
        public long ContactId { get; set; }
        [Required]
        [StringLength(100)]
        public string Value { get; set; }
        [Required]
        [Column(@"Communication_Type")]
        public long CommunicationType { get; set; }
        [ForeignKey("CommunicationType")] public virtual DomainEnum CommunicationTypeEnum { get; set; }
        [ForeignKey("ContactId")] public virtual Contact Contact { get; set; }

    }
}
