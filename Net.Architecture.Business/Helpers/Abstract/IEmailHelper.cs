using Net.Architecture.Entities.Views;

namespace Net.Architecture.Business.Helpers.Abstract
{
    public interface IEmailHelper
    {
        void SendMailWithDefaultOutlook(MailContentView mailContent);
    }
}
