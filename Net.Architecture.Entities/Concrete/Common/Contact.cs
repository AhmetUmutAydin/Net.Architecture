using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Concrete.Common
{
    // Instead of creating another communication table or using multiple fk in communication, if there is a need to save contact information for a branch, institution or anything else.
    // A relationship can be established between this table and the required table.
    // Ex: ContactTable: ContactId 1 - CommunicationId 1 , EmployeeTable: ContactId 1
    [Table("Contact", Schema = "Common")]
    public class Contact : BaseEntity
    {
        public virtual PersonalInformation PersonalInformation { get; set; }
        public virtual ICollection<Communication> Communication { get; set; }
    }
}
