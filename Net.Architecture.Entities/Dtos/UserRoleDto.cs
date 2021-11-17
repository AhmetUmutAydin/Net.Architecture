using System.Collections.Generic;
using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class UserRoleDto : DtoBase
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public List<DropdownDto> Roles { get; set; }
    }
}
