using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class TrainingTypeDto : DtoBase
    {
        public bool Status { get; set; }
        public string Name { get; set; }
    }
}
