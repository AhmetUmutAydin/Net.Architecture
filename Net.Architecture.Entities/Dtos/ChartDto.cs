using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class ChartDto : IDTO
    {
        public string Name { get; set; }
        public double FieldOne { get; set; }
        public double FieldTwo { get; set; }

    }
}
