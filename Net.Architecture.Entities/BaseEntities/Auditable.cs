using System;
using System.ComponentModel.DataAnnotations.Schema;
using Net.Architecture.Entities.Concrete.Auth;

namespace Net.Architecture.Entities.BaseEntities
{
    public abstract class Auditable : BaseEntity
    {
        public DateTime? CreateDate { get; set; }
        public long? CreateUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }

        [ForeignKey("CreateUserId")] public virtual User CreateUser { get; set; }
        [ForeignKey("UpdateUserId")] public virtual User UpdateUser { get; set; }

    }
}
