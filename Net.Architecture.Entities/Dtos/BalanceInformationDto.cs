using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class BalanceInformationDto : DtoBase
    {
        public string FullName { get; set; }
        public double Balance { get; set; }
    }
}
