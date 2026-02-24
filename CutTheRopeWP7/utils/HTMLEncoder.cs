using System.Net;

namespace ctr_wp7.utils
{
    // Token: 0x020000C8 RID: 200
    public class HTMLEncoder
    {
        // Token: 0x060005D2 RID: 1490 RVA: 0x0002C796 File Offset: 0x0002A996
        public static string encode(string s)
        {
            return WebUtility.UrlEncode(s);
        }
    }
}
