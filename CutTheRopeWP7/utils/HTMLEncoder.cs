using System.Net;

namespace ctr_wp7.utils
{
    public class HTMLEncoder
    {
        public static string encode(string s)
        {
            return WebUtility.UrlEncode(s);
        }
    }
}
