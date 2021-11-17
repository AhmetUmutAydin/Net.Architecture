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

        protected long JwtInstitutionId
        {
            get
            {
                return long.Parse(User.FindFirst("InstitutionId").Value);
            }
        }

        protected long JwtUserId
        {
            get
            {
                return long.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            }
        }

        protected long JwtEmployeeId
        {
            get
            {
                return long.Parse(User.FindFirst("EmployeeId").Value);
            }
        }

        protected long? JwtTrainerEmployeeId
        {
            get
            {
                if (User.FindFirst(ClaimTypes.Role).Value == Constants.TrainerRoleName)
                    return JwtEmployeeId;
                else
                    return null;
            }
        }

        protected long? JwtCompanyInstitutionId
        {
            get
            {
                if (User.FindFirst(ClaimTypes.Role)?.Value == Constants.CompanyRoleName)
                    return JwtInstitutionId;
                else
                    return null;
            }
        }

        protected long? JwtDemoInstitutionId
        {
            get
            {
                if (User.FindFirst(ClaimTypes.Role)?.Value == Constants.DemoRoleName)
                    return JwtInstitutionId;
                else
                    return null;
            }
        }

        protected bool JwtIsCompanyUser
        {
            get
            {
                if (User.FindFirst(ClaimTypes.Role).Value == Constants.CompanyRoleName)
                    return true;
                else
                    return false;
            }
        }
    }
}
