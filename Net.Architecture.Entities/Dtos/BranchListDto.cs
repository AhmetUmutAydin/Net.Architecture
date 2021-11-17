using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class BranchListDto: DtoBase
    {
        public string Name { get; set; }
        public bool? IsItCentralBranch { get; set; }
        public string CityName { get; set; }
    }
}
