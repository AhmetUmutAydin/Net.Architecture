using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class LoginDto:IDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ModuleRole { get; set; }
    }
}
