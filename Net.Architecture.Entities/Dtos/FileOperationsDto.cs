using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class FileOperationsDto : IDTO
    {
        public FileDto File { get; set; }
        public DeciderDto Decider { get; set; }
    }
}
