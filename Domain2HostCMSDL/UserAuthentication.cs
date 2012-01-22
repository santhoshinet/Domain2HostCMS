using Telerik.OpenAccess;

namespace Domain2HostCMSDL
{
    [Persistent]
    public class UserAuthentication
    {
        public string Username { get; set; }

        public string Domain { get; set; }
    }
}