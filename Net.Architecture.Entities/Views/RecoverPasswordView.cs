using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Views
{
    public class RecoverPasswordView:IView
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}
