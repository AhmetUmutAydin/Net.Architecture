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
    public class DistrictManager : BaseBusiness, IDistrictService
    {
        private readonly ICacheManager _cacheManager;

        public DistrictManager(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public async Task<IServiceResult<IEnumerable<DropdownDto>>> GetDistricts(long cityId)
        {
            var districts = (await _cacheManager.GetEntities<District>()).Where(x => x.CityId == cityId);
            var result = districts.ToDtos<DropdownDto>();
            return new ServiceResult<IEnumerable<DropdownDto>>(result);
        }
    }
}
