using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Concrete.Common
{
    [Table("City", Schema = "Common")]
    public class City : BaseEntityNone
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<District> District { get; set; }
        public virtual ICollection<Address> Address { get; set; }
    }
}
