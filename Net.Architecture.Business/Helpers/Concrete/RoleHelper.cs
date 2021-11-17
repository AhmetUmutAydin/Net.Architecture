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

        public async Task<IServiceResult> SaveUserRole(long userId, long employeeType)
        {
            var roleId = await FindRoleId(employeeType);
            var userRole = await _unitOfWork.Repository<UserRoleDal>().GetAsync(x => x.Status && x.UserId == userId);

            if (userRole is null)
            {
                userRole = new UserRole()
                {
                    Status = true,
                    UserId = userId,
                    RoleId = roleId
                };

                await _unitOfWork.Repository<UserRoleDal>().AddAsync(userRole);
            }
            else
            {
                userRole.RoleId = roleId;
                _unitOfWork.Repository<UserRoleDal>().Update(userRole);
            }

            await _unitOfWork.SaveChangesAsync();
            return new ServiceResult();
        }

        private async Task<long> FindRoleId(long employeeType)
        {
            long roleId = 0;
            if (employeeType == (long)Enums.EmployeeType.Owner)
            {
                var role = await _unitOfWork.Repository<RoleDal>().GetAsync(x => x.Status && x.Name == Constants.CompanyRoleName);
                roleId = role.Id;
            }

            else if (employeeType == (long)Enums.EmployeeType.Trainer)
            {
                var role = await _unitOfWork.Repository<RoleDal>().GetAsync(x => x.Status && x.Name == Constants.TrainerRoleName);
                roleId = role.Id;
            }

            return roleId;
        }
    }
}
