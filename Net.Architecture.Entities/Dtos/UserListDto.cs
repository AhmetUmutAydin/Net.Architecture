using System;
using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class UserListDto : DtoBase
    {
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
