using System;
using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class FilterStatisticsDto : IDTO
    {
        public DateTime StartingDate { get; set; }
        public DateTime EndDate { get; set; }
        public long? PersonalInformationId { get; set; }
        public long? BranchId { get; set; }
    }
}
