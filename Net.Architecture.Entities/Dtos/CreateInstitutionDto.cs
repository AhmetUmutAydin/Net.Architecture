using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class CreateInstitutionDto : IDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool MemberConfirmationPayment { get; set; } = false;
    }
}
