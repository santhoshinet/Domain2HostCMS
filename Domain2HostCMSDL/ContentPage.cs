using Telerik.OpenAccess;

namespace Domain2HostCMSDL
{
    [Persistent]
    public class ContentPage
    {
        public string Name { get; set; }

        public string Content { get; set; }

        public string Id { get; set; }

        public string DomainName { get; set; }
    }
}