using System.Collections.Generic;
using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class DashboardDto : IDTO
    {
        public int DailyTrainingCount { get; set; }
        public int TotalTrainingCount { get; set; }
        public int TotalTrainerCount { get; set; }
        public int TotalMemberCount { get; set; }

        public int FilteredTrainingCount { get; set; }
        public double FilteredExpense { get; set; }//Hocaya yapılan aylık ödeme
        public double FilteredIncome { get; set; }// Aylık öğrencilerin harcadığı bakiye
        public double FilteredAddedBalance{ get; set; } // EKlenen bakiye
        public int FilteredAddedBalanceCount { get; set; }//Eklenen bakiye sayısı

        public IEnumerable<ChartDto> FilteredChartValues { get; set; }

        //public int TrainerDeservedInfo { get; set; }//Eğitmenin hakettiği ama almadığı para
        //public int TrainerMonthlyIncome { get; set; }//Eğitmenin aylık aldığı para
    }
}
