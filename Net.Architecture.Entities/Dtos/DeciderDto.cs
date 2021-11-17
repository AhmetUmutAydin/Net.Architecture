using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class DeciderDto : IDTO
    {
        public long DeciderId { get; set; }
        public long DeciderType { get; set; }
    }
}
