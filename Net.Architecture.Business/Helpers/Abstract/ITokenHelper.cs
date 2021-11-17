using System.Collections.Generic;
using Net.Architecture.Entities.Concrete.Auth;
using Net.Architecture.Entities.Configurations;
using Net.Architecture.Entities.Dtos;

namespace Net.Architecture.Business.Helpers.Abstract
{
    public interface ITokenHelper
    {
        TokenDto CreateToken(User user, IEnumerable<Role> roles);
        AccessTokenDto CreateTokenForClient(Client client);
    }
}
