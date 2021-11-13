using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net.Architecture.Entities.BaseEntities
{
    public abstract class BaseEntity<T> : IEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public T Id { get; set; }
        [Required]
        public bool Status { get; set; }
    }
    public abstract class BaseEntity : BaseEntity<long>
    {
        public BaseEntity():base()
        {
            
        }
    }
}
