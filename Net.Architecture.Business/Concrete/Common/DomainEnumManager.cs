using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Net.Architecture.Business.Abstract;
using Net.Architecture.Business.Abstract.Common;
using Net.Architecture.Core.CrossCuttingConcerns.Caching;
using Net.Architecture.Core.Utilities.Result;
using Net.Architecture.Entities.Concrete.Common;

namespace Net.Architecture.Business.Concrete.Common
{
    public class DomainEnumManager : BaseBusiness, IDomainEnumService
    {
        private readonly ICacheManager _cacheManager;

        public DomainEnumManager(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }


        public async Task<IServiceResult<IEnumerable<DropdownDto>>> GetChildEnums(long parentId)
        {
            var enums = (await _cacheManager.GetEntities<DomainEnum>()).Where(x => x.ParentId == parentId);
            var result = enums.ToDtos<DropdownDto>();
            return new ServiceResult<IEnumerable<DropdownDto>>(result);
        }
    }
}

