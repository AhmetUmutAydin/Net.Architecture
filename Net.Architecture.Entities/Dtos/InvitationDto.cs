using System;
using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class InvitationDto : DtoBase<Guid>
    {
        public long? EmployeeId { get; set; }
        public long? TrainingDetailId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public long InvitationType { get; set; }
        public long CommunicationType { get; set; }
        public string CommunicationValue { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
