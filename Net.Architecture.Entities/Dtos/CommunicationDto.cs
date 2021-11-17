using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class CommunicationDto : DtoBase
    {
        public long? ContactId { get; set; }
        public string Value { get; set; }
        public long CommunicationType { get; set; }
    }
}

