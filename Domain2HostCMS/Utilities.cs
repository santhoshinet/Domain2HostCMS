namespace Domain2HostCMS
{
    public class Utilities
    {
        public static string GetMyDomain(System.Uri uri)
        {
            return uri.Host;
        }
    }
}