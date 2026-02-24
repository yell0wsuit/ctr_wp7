using System.Text;

using ctr_wp7.utils;

namespace ctr_wp7.remotedata.cartoons
{
    // Token: 0x02000098 RID: 152
    public class LinkBuilder(string start)
    {

        // Token: 0x0600048A RID: 1162 RVA: 0x00020BE8 File Offset: 0x0001EDE8
        public void put(string key, object value)
        {
            if (skipampersand)
            {
                skipampersand = false;
            }
            else
            {
                _ = link.Append('&');
            }
            _ = link.Append(key);
            _ = link.Append('=');
            if (value is string)
            {
                value = HTMLEncoder.encode((string)value);
            }
            _ = link.Append(value);
        }

        // Token: 0x0600048B RID: 1163 RVA: 0x00020C57 File Offset: 0x0001EE57
        public override string ToString()
        {
            return link.ToString();
        }

        // Token: 0x040009CE RID: 2510
        private StringBuilder link = new StringBuilder(start);

        // Token: 0x040009CF RID: 2511
        private bool skipampersand = start.EndsWith("?");
    }
}
