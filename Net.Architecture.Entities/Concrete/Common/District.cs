using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Concrete.Common
{
    [Table("District", Schema = "Common")]
    public class District : BaseEntityNone
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Column(@"City_Id")]
        public long CityId { get; set; }
        [ForeignKey("CityId")] public virtual City City { get; set; }
        public virtual ICollection<Address> Address { get; set; }

    }
}
