using System;
using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class TrainingConfirmationDto : IDTO
    {
        public string EmployeeFullName { get; set; }
        public string MemberFullName { get; set; }
        public string TrainingTypeName { get; set; }
        public DateTime TrainingDate { get; set; }
    }
}