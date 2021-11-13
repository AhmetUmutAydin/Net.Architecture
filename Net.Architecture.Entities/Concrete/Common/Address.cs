using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Concrete.Common
{
    [Table("Address", Schema = "Common")]
    public class Address : Auditable
    {
        [Required]
        [StringLength(200)]
        public string Description { get; set; }
        [Required]
        [Column(@"City_Id")]
        public long CityId { get; set; }
        [Column(@"District_Id")]
        public long DistrictId { get; set; }
        [ForeignKey("CityId")] public virtual City City { get; set; }
        [ForeignKey("DistrictId")] public virtual District District { get; set; }

        public virtual PersonalInformation PersonalInformation { get; set; }
    }
}
