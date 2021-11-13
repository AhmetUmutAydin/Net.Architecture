using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net.Architecture.Entities.BaseEntities
{
    public abstract class BaseEntityNone<T> : IEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public T Id { get; set; }
        [Required]
        public bool Status { get; set; }
    }
    public abstract class BaseEntityNone : BaseEntityNone<long>
    {
        public BaseEntityNone() : base()
        {

        }
    }
}
