using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class InstitutionDto : DtoBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool MemberConfirmationPayment { get; set; }
        public bool IsActiveMemberConfirmationPayment { get; set; }
    }
}
