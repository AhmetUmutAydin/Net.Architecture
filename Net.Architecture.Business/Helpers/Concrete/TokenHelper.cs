using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Net.Architecture.Business.Abstract;
using Net.Architecture.Business.Helpers.Abstract;
using Net.Architecture.Core.Extensions;
using Net.Architecture.Core.Utilities.Security.Encryption;
using Net.Architecture.Entities.Concrete.Auth;
using Net.Architecture.Entities.Configurations;
using Net.Architecture.Entities.Dtos;

namespace Net.Architecture.Business.Helpers.Concrete
{
    public class TokenHelper :BaseBusiness, ITokenHelper
    {
        private readonly TokenOptions _tokenOptions;

        public TokenHelper(IOptions<TokenOptions> options)
        {
            _tokenOptions = options.Value;
        }

        #region UserToken
        public TokenDto CreateToken(User user, IEnumerable<Role> roles)
        {
            var refreshTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.RefreshTokenExpiration);
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigninCredentials(securityKey);

            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, accessTokenExpiration, roles);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return CreateRefreshToken(token, accessTokenExpiration, refreshTokenExpiration);
        }
        private TokenDto CreateRefreshToken(string token, DateTime expiration, DateTime refreshTokenExpiration)
        {
            TokenDto tokenDto = new TokenDto()
            {
                Expiration = expiration,
                Token = token,
                RefreshToken = Guid.NewGuid().ToString(),
                RefreshTokenExpiration = refreshTokenExpiration
            };
            return tokenDto;
        }
        private JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user, SigningCredentials signingCredentials, DateTime accessTokenExpiration, IEnumerable<Role> roles)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience.ToString(),
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user, roles, tokenOptions.Audience),
                signingCredentials: signingCredentials);
            return jwt;
        }
        private IEnumerable<Claim> SetClaims(User user, IEnumerable<Role> roles, List<string> audiences)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName(user.Username);
            claims.AddRoles(roles.Select(c => c.Name).ToList());
            claims.AddJti();
            claims.AddAudiences(audiences);
            claims.AddInstitutionId(user.Employee.InstitutionId.ToString());
            claims.AddEmployeeId(user.Employee.Id.ToString());
            return claims;
        }
        #endregion
        #region ClientToken
        public AccessTokenDto CreateTokenForClient(Client client)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigninCredentials(securityKey);

            var jwt = CreateJwtSecurityTokenForClient(_tokenOptions, client, signingCredentials, accessTokenExpiration);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessTokenDto
            {
                Token = token,
                Expiration = accessTokenExpiration
            };
        }
        private JwtSecurityToken CreateJwtSecurityTokenForClient(TokenOptions tokenOptions, Client client, SigningCredentials signingCredentials, DateTime accessTokenExpiration)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: client.Audience.ToString(),
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaimsForClient(client, client.Audience),
                signingCredentials: signingCredentials);
            return jwt;
        }
        private IEnumerable<Claim> SetClaimsForClient(Client client, List<string> audiences)
        {
            var claims = new List<Claim>();
            claims.AddSub(client.Id);
            claims.AddJti();
            claims.AddAudiences(audiences);
            return claims;
        }
        #endregion

    }
}
