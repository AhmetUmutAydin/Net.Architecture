using System;
using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class MemberListDto : DtoBase
    {
        public string FullName { get; set; }
        public DateTime StartingDate { get; set; }
        public double Balance { get; set; }
        public string Notes { get; set; }
    }
}
