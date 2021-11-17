using System;

namespace Net.Architecture.Entities.Dtos
{
    public class TokenDto:AccessTokenDto
    {
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
