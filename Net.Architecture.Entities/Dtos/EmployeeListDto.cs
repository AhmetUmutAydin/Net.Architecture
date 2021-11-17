using System;
using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class EmployeeListDto:DtoBase
    {
        public string EmployeeTypeName { get; set; }
        public long EmployeeType { get; set; }
        public string FullName { get; set; }
        public DateTime StartingDate { get; set; }
        public double Balance { get; set; }
        public bool UserExists { get; set; }
        public string Notes { get; set; }

    }
}
