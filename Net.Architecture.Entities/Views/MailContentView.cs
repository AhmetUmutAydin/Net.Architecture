using System.Collections.Generic;
using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Views
{
    public class MailContentView:IView
    {
        public List<string> ToList { get; set; }
        public string Body { get; set; }
        public string Title { get; set; }
    }
}
