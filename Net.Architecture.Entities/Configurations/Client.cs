using System.Collections.Generic;

namespace Net.Architecture.Entities.Configurations
{
    public class Client
    {
        public string Id { get; set; }
        public string Secret { get; set; }
        public List<string> Audience { get; set; }
    }
}
