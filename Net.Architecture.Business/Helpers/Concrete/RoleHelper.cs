using System.Threading.Tasks;
using Net.Architecture.Business.Abstract;
using Net.Architecture.Business.Helpers.Abstract;
using Net.Architecture.Core.Utilities.Result;
using Net.Architecture.DataAccess.UnitOfWork;
using Net.Architecture.Entities.Concrete.Auth;
using Net.Architecture.Entities.Enums;

namespace Net.Architecture.Business.Helpers.Concrete
{
    public class RoleHelper : BaseBusiness, IRoleHelper
    {
        private readonly IUnitOfWork _unitOfWork;
        public RoleHelper(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IServiceResult> SaveUserRole(long userId, string role)
        {
            var roleId = await FindRoleId(role);
            var userRole = await _unitOfWork.Repository<UserRole>().GetAsync(x => x.Status && x.UserId == userId);

            if (userRole is null)
            {
                userRole = new UserRole()
                {
                    Status = true,
                    UserId = userId,
                    RoleId = roleId
                };

                await _unitOfWork.Repository<UserRole>().AddAsync(userRole);
            }
            else
            {
                userRole.RoleId = roleId;
                _unitOfWork.Repository<UserRole>().Update(userRole);
            }

            await _unitOfWork.SaveChangesAsync();
            return new ServiceResult();
        }

        private async Task<long> FindRoleId(string role)
        {
            var selectedRole = await _unitOfWork.Repository<Role>().GetAsync(x => x.Status && x.Name == role);
            return selectedRole.Id;
        }
    }
}
