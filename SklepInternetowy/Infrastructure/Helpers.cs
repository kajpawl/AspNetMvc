using System.Net;

namespace SklepInternetowy.Infrastructure
{
    internal class Helpers
    {
        public static void CallUrl(string url)
        {
            var req = HttpWebRequest.Create(url);
            req.GetResponseAsync();
        }
    }
}