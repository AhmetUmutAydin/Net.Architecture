using System.Collections.Generic;
using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class BalanceMovementsDto:IDTO
    {
        public double FilteredExpense { get; set; }
        public double FilteredIncome { get; set; }
        public IEnumerable<BalanceListDto> BalanceList { get; set; }
        public IEnumerable<ChartDto> FilteredChartValues { get; set; }
    }
}
