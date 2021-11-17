using System;
using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class MemberDto:DtoBase
    {
        public DateTime StartingDate { get; set; }
        public DateTime? LeavingDate { get; set; }
    }
}

