using System;
using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class BalanceListDto : DtoBase
    {       
        public long BalanceType { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Description { get; set; }
        public string FullName { get; set; }
        public long DeciderType { get; set; }
        public string DeciderDescription { get; set; }
        public string CreatedUser  { get; set; }
    }
}
