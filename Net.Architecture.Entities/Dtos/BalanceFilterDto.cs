using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class BalanceFilterDto : IDTO
    {
        public FilterStatisticsDto FilterStatisticsDto { get; set; }
        public DeciderDto DeciderDto { get; set; }
    }
}
