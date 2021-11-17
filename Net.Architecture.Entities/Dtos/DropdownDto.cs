using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class DropdownDto : IDTO
    {
        public string Text { get; set; }
        public long Value { get; set; }
    }
}
