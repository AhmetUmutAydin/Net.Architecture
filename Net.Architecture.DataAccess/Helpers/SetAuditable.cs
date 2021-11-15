using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.DataAccess.Helpers
{
    public static class SetAuditable
    {
        private static IHttpContextAccessor _httpContextAccessor;
        public static void SetHttpContextAccessor(IHttpContextAccessor accessor)
        {
            _httpContextAccessor = accessor;
        }

        private static ClaimsPrincipal User
        {
            get
            {
                return _httpContextAccessor.HttpContext.User;
            }
        }

        private static long? JwtUserId
        {
            get
            {
                if(User.FindFirst(ClaimTypes.NameIdentifier) is null)
                    return null;           
                return long.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            }
        }


        public static void SetAuditablCreate<T>(ref T model)
        {
            if (typeof(Auditable).IsAssignableFrom(typeof(T)))
            {
                var auiditable = model as Auditable;
                auiditable.CreateDate = DateTime.Now;
                auiditable.CreateUserId = JwtUserId;
            }

        }

        public static void SetAuditablUpdate<T>(ref T model) 
        {
            if (typeof(Auditable).IsAssignableFrom(typeof(T)))
            {
                var auiditable = model as Auditable;
                auiditable.UpdateDate = DateTime.Now;
                auiditable.UpdateUserId = JwtUserId;
            }
        }
    }
}
