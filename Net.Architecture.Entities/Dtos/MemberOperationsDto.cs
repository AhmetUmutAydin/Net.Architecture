using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class MemberOperationsDto:IDTO
    {
        public PersonalInformationDto PersonalInformationDto { get; set; }
        public MemberDto MemberDto { get; set; }
    }
}
