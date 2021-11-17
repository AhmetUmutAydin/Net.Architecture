using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class RecoverPasswordDto : IDTO
    {
        public string EmailOrUsername { get; set; }
    }
}
