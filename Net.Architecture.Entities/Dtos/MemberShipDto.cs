using System;
using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class MembershipDto : DtoBase
    {
        public long InstitutionId { get; set; }
        public short AllowedEmployeeCount { get; set; }
        public short AllowedMemberCount { get; set; }
        public short AllowedBranchCount { get; set; }
        public short PresentEmployeeCount { get; set; }
        public short PresentMemberCount { get; set; }
        public short PresentBranchCount { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime ExpiredDate { get; set; }
        public bool IsActiveMemberConfirmationPayment { get; set; }
    }
}
