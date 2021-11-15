using System;
using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class PersonalInformationDto : DtoBase
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public long? NationalIdentifier { get; set; }
        public string BirthPlace { get; set; }
        public DateTime? Birthdate { get; set; }
        public long? Gender { get; set; }
        public string ImageUrl { get; set; }
        public string Notes { get; set; }

    }
}
