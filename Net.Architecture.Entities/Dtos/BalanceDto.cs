using System;
using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class BalanceDto : DtoBase
    {
        public long DeciderId { get; set; }
        public long BalanceType { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Description { get; set; }
    }
}
