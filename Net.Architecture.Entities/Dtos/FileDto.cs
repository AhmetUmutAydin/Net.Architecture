using Microsoft.AspNetCore.Http;
using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class FileDto : DtoBase
    {
        public string Source { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
