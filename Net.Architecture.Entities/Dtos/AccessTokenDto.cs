using System;
using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class AccessTokenDto : IDTO
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
