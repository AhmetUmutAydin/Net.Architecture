using System;
using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class EmployeeDto : DtoBase
    {
        public DateTime StartingDate { get; set; }
        public DateTime? LeavingDate { get; set; }
        public long EmployeeType { get; set; }
        public string Certificates { get; set; }
    }
}
