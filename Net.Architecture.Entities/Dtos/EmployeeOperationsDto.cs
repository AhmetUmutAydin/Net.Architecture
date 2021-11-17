using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class EmployeeOperationsDto : IDTO
    {
        public PersonalInformationDto PersonalInformationDto { get; set; }
        public EmployeeDto EmployeeDto { get; set; }
    }
}
