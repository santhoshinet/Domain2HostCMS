namespace Domain2HostCMS
{
    public class Utilities
    {
        public static string GetMyDomain(System.Uri uri)
        {
            //return "admin";
            var domains = uri.Host.Split('.');
            if (domains.Length > 0)
                return domains[0];
            return uri.Host;
        }
    }
}