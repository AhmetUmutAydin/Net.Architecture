using System;
using System.Collections.Generic;
using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
     public class TrainingDto:DtoBase
    {
        public long EmployeeId { get; set; }
        public long TrainingType { get; set; }
        public IEnumerable<long> MemberIdList { get; set; }
        public long BranchId { get; set; }
        public DateTime TrainingDate { get; set; }
    }
}
