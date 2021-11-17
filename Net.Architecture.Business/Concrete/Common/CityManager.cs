using System.Collections.Generic;
using System.Threading.Tasks;
using Net.Architecture.Business.Abstract;
using Net.Architecture.Business.Abstract.Common;
using Net.Architecture.Core.CrossCuttingConcerns.Caching;
using Net.Architecture.Core.Utilities.Result;
using Net.Architecture.Entities.Concrete.Common;

namespace Net.Architecture.Business.Concrete.Common
{
    public class CityManager : BaseBusiness, ICityService
    {
        private readonly ICacheManager _cacheManager;

        public CityManager(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public async Task<IServiceResult<IEnumerable<DropdownDto>>> GetCities()
        {
            var cities = await _cacheManager.GetEntities<City>();
            var result = cities.ToDtos<DropdownDto>();
            return new ServiceResult<IEnumerable<DropdownDto>>(result);
        }
    }
}
