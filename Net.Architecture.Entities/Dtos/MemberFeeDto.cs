using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class MemberFeeDto : DtoBase
    {
        public long MemberId { get; set; }
        public long TrainingTypeId { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
    }
}
