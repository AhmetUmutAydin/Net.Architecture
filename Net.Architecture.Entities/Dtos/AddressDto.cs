using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class AddressDto : DtoBase
    {
        public string Description { get; set; }
        public long CityId { get; set; }
        public long DistrictId { get; set; }
        public DeciderDto Decider { get; set; }
    }
}
