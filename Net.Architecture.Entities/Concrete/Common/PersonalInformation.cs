using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Concrete.Common
{
    [Table("Personal_Information", Schema = "Common")]
    public class PersonalInformation : Auditable
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Surname { get; set; }
        [MinLength(11)]
        [MaxLength(11)]
        [Column(@"National_Identifier")]
        public long? NationalIdentifier { get; set; }
        [StringLength(100)]
        [Column(@"Birth_Place")]
        public string BirthPlace { get; set; }
        public DateTime? Birthdate { get; set; }
        public long? Gender { get; set; }
        [StringLength(200)]
        public string Notes { get; set; }
        [Column(@"Contact_Id")]
        public long? ContactId { get; set; }
        [Column(@"Address_Id")]
        public long? AddressId { get; set; }
        [Column(@"Picture_Id")]
        public long? PictureId { get; set; }

        [ForeignKey("Gender")] public virtual DomainEnum GenderEnum { get; set; }
        [ForeignKey("ContactId")] public virtual Contact Contact { get; set; }
        [ForeignKey("AddressId")] public virtual Address Address { get; set; }
        [ForeignKey("PictureId")] public virtual File File { get; set; }
    }
}
