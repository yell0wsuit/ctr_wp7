using System;
using System.Collections.Generic;

namespace ctr_wp7.ios
{
    // Token: 0x020000CA RID: 202
    internal sealed class NSString : NSObject
    {
        // Token: 0x060005D4 RID: 1492 RVA: 0x0002C7A6 File Offset: 0x0002A9A6
        public NSString()
        {
        }

        // Token: 0x060005D5 RID: 1493 RVA: 0x0002C7AE File Offset: 0x0002A9AE
        public NSString(string rhs)
        {
            value_ = rhs;
        }

        // Token: 0x060005D6 RID: 1494 RVA: 0x0002C7BD File Offset: 0x0002A9BD
        public override string ToString()
        {
            return value_;
        }

        // Token: 0x060005D7 RID: 1495 RVA: 0x0002C7C5 File Offset: 0x0002A9C5
        public int length()
        {
            if (value_ == null)
            {
                return 0;
            }
            return value_.Length;
        }

        // Token: 0x060005D8 RID: 1496 RVA: 0x0002C7DC File Offset: 0x0002A9DC
        public bool isEqualToString(NSString str)
        {
            return isEqualToString(str.value_);
        }

        // Token: 0x060005D9 RID: 1497 RVA: 0x0002C7EA File Offset: 0x0002A9EA
        public bool isEqualToString(string str)
        {
            if (value_ == null)
            {
                return str == null;
            }
            return str != null && value_ == str;
        }

        // Token: 0x060005DA RID: 1498 RVA: 0x0002C80A File Offset: 0x0002AA0A
        public int IndexOf(char c)
        {
            return value_.IndexOf(c);
        }

        // Token: 0x060005DB RID: 1499 RVA: 0x0002C818 File Offset: 0x0002AA18
        public NSRange rangeOfString(NSString str)
        {
            return rangeOfString(str.value_);
        }

        // Token: 0x060005DC RID: 1500 RVA: 0x0002C828 File Offset: 0x0002AA28
        public NSRange rangeOfString(string str)
        {
            NSRange nsrange;
            nsrange.length = 0U;
            nsrange.location = 0U;
            if (str.Length > 0)
            {
                int num = value_.IndexOf(str);
                if (num > -1)
                {
                    nsrange.length = (uint)str.Length;
                    nsrange.location = (uint)num;
                }
            }
            return nsrange;
        }

        // Token: 0x060005DD RID: 1501 RVA: 0x0002C875 File Offset: 0x0002AA75
        public char characterAtIndex(int n)
        {
            return value_[n];
        }

        // Token: 0x060005DE RID: 1502 RVA: 0x0002C883 File Offset: 0x0002AA83
        public NSString copy()
        {
            return new NSString(value_);
        }

        // Token: 0x060005DF RID: 1503 RVA: 0x0002C890 File Offset: 0x0002AA90
        public void getCharacters(char[] to)
        {
            int num = Math.Min(to.Length - 1, length());
            for (int i = 0; i < num; i++)
            {
                to[i] = value_[i];
            }
            to[num] = '\0';
        }

        // Token: 0x060005E0 RID: 1504 RVA: 0x0002C8D0 File Offset: 0x0002AAD0
        public char[] getCharacters()
        {
            int num = length();
            char[] array = new char[num + 1];
            getCharacters(array);
            return array;
        }

        // Token: 0x060005E1 RID: 1505 RVA: 0x0002C8F5 File Offset: 0x0002AAF5
        public NSString substringWithRange(NSRange range)
        {
            return new NSString(value_.Substring((int)range.location, (int)range.length));
        }

        // Token: 0x060005E2 RID: 1506 RVA: 0x0002C915 File Offset: 0x0002AB15
        public NSString substringFromIndex(int n)
        {
            return new NSString(value_[n..]);
        }

        // Token: 0x060005E3 RID: 1507 RVA: 0x0002C928 File Offset: 0x0002AB28
        public NSString substringToIndex(int n)
        {
            return new NSString(value_[..n]);
        }

        // Token: 0x060005E4 RID: 1508 RVA: 0x0002C93C File Offset: 0x0002AB3C
        public int intValue()
        {
            if (value_.Length == 0)
            {
                return 0;
            }
            int num = 0;
            int i = 0;
            int length = value_.Length;
            int num2 = 1;
            while (i < length)
            {
                if (value_[i] == ' ')
                {
                    i++;
                }
                else if (value_[i] == '-')
                {
                    num2 = -1;
                    i++;
                }
                else
                {
                    num *= 10;
                    num += value_[i++] - '0';
                }
            }
            return num * num2;
        }

        // Token: 0x060005E5 RID: 1509 RVA: 0x0002C9BC File Offset: 0x0002ABBC
        public bool boolValue()
        {
            if (value_.Length == 0)
            {
                return false;
            }
            string text = value_.ToLower();
            return text == "true";
        }

        // Token: 0x060005E6 RID: 1510 RVA: 0x0002C9F0 File Offset: 0x0002ABF0
        public float floatValue()
        {
            if (value_.Length == 0)
            {
                return 0f;
            }
            float num = 0f;
            int i = 0;
            int length = value_.Length;
            int num2 = 1;
            int num3 = 10;
            int num4 = 1;
            while (i < length)
            {
                if (value_[i] == ' ')
                {
                    i++;
                }
                else if (value_[i] == '-')
                {
                    num2 = -1;
                    i++;
                }
                else if (value_[i] is ',' or '.')
                {
                    num3 = 1;
                    num4 = 10;
                    i++;
                }
                else
                {
                    num *= num3;
                    num += (value_[i++] - 48f) / num4;
                    if (num4 > 1)
                    {
                        num4 *= 10;
                    }
                }
            }
            return num * num2;
        }

        // Token: 0x060005E7 RID: 1511 RVA: 0x0002CAC8 File Offset: 0x0002ACC8
        public List<NSString> componentsSeparatedByString(char ch)
        {
            List<NSString> list = [];
            char[] array = [ch];
            string[] array2 = value_.Split(array);
            foreach (string text in array2)
            {
                list.Add(new NSString(text));
            }
            return list;
        }

        // Token: 0x060005E8 RID: 1512 RVA: 0x0002CB1C File Offset: 0x0002AD1C
        public bool hasPrefix(NSString prefix)
        {
            return value_.StartsWith(prefix.ToString());
        }

        // Token: 0x04000B3D RID: 2877
        private string value_;
    }
}
