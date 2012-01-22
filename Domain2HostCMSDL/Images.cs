using Telerik.OpenAccess;

namespace Domain2HostCMSDL
{
    [Persistent]
    public class File
    {
        public byte[] Filedata { get; set; }

        public string Filename { get; set; }

        public string MimeType { get; set; }

        public string Id { get; set; }

        public string DomainName { get; set; }
    }
}