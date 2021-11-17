using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class StudioRegisterDto : IDTO
    {
        public CreateInstitutionDto CreateInstitutionDto { get; set; }
        public CreateUserDto CreateUserDto { get; set; }
        public CreatePaymentDto CreatePaymentDto { get; set; }
    }
}
