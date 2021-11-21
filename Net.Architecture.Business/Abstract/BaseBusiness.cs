using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Net.Architecture.Core.Constants;
using Net.Architecture.Core.Utilities.IoC;

namespace Net.Architecture.Business.Abstract
{
    public abstract class BaseBusiness
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BaseBusiness()
        {
            _httpContextAccessor = IocManager.Resolve<IHttpContextAccessor>();
        }

        private ClaimsPrincipal User
        {
            get
            {
                return _httpContextAccessor.HttpContext.User;
            }
        }

        protected long JwtUserId
        {
            get
            {
                return long.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            }
        }
    }
}
