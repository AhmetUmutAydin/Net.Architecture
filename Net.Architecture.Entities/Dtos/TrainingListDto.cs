using System;
using System.Collections.Generic;
using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class TrainingListDto : DtoBase
    {
        public long EmployeeId { get; set; }
        public string TrainerFullName { get; set; }
        public string BranchName { get; set; }
        public string TrainingTypeName { get; set; }
        public DateTime TrainingDate { get; set; }
        public double BalanceAmount { get; set; }
        public IEnumerable<string> MemberFullNameList { get; set; }
        public bool MemberConfirmation { get; set; }
    }
}
