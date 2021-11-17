using System.Collections.Generic;
using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class ContactDto:IDTO
    {
        public IEnumerable<CommunicationDto> Communications { get; set; }
        public DeciderDto Decider { get; set; }
    }
}
