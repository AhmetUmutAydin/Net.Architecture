using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Net.Architecture.Core.Extensions
{
    public static class ClaimExtensions
    {
        public static void AddEmail(this ICollection<Claim> claims, string email)
        {
            claims.Add(new Claim(ClaimTypes.Email, email));
        }
        public static void AddName(this ICollection<Claim> claims, string name)
        {
            claims.Add(new Claim(ClaimTypes.Name, name));
        }
        public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier)
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, nameIdentifier));
        }
        public static void AddRoles(this ICollection<Claim> claims, List<string> roles)
        {
            roles.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
        }
        public static void AddJti(this ICollection<Claim> claims)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        }
        public static void AddSub(this ICollection<Claim> claims, string clientId)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, clientId));
        }
        public static void AddAudiences(this ICollection<Claim> claims, List<string> audiences)
        {
            audiences.ForEach(audience => claims.Add(new Claim(JwtRegisteredClaimNames.Aud, audience)));
        }
        public static void AddInstitutionId(this ICollection<Claim> claims, string institutionId)
        {
            claims.Add(new Claim("institutionId", institutionId));
        }
        public static void AddEmployeeId(this ICollection<Claim> claims, string employeeId)
        {
            claims.Add(new Claim("employeeId", employeeId));
        }
    }
}
