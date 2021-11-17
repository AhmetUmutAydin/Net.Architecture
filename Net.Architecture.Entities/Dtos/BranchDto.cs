using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class BranchDto : DtoBase
    {
        public string Name { get; set; }
        public bool? IsItCentralBranch { get; set; }
        public long? InstitutionId { get; set; }
        public string ImageUrl { get; set; }
    }
}
