using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class ClientLoginDto : IDTO
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
