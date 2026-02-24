using System.Text;

using ctr_wp7.utils;

namespace ctr_wp7.remotedata.cartoons
{
    public class LinkBuilder(string start)
    {

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

        public override string ToString()
        {
            return link.ToString();
        }

        private readonly StringBuilder link = new(start);

        private bool skipampersand = start.EndsWith("?");
    }
}
