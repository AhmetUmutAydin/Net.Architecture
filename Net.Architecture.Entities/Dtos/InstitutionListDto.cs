using System;
using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class InstitutionListDto : DtoBase
    {
        public string Name { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime ExpiredDate { get; set; }
        public bool Status { get; set; }
    }
}
