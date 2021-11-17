using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class CreateInstitutionAndUserOperationsDto : IDTO
    {
        public CreateInstitutionDto CreateInstitutionDto { get; set; }
        public CreateUserDto CreateUserDto { get; set; }
    }
}
