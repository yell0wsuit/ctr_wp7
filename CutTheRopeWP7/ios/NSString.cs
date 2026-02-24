using System;
using System.Collections.Generic;

namespace ctr_wp7.ios
{
    internal sealed class NSString : NSObject
    {
        public NSString()
        {
        }

        public NSString(string rhs)
        {
            value_ = rhs;
        }

        public override string ToString()
        {
            return value_;
        }

        public int length()
        {
            return value_ == null ? 0 : value_.Length;
        }

        public bool isEqualToString(NSString str)
        {
            return isEqualToString(str.value_);
        }

        public bool isEqualToString(string str)
        {
            return value_ == null ? str == null : str != null && value_ == str;
        }

        public int IndexOf(char c)
        {
            return value_.IndexOf(c);
        }

        public NSRange rangeOfString(NSString str)
        {
            return rangeOfString(str.value_);
        }

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

        public char characterAtIndex(int n)
        {
            return value_[n];
        }

        public NSString copy()
        {
            return new NSString(value_);
        }

        public void getCharacters(char[] to)
        {
            int num = Math.Min(to.Length - 1, length());
            for (int i = 0; i < num; i++)
            {
                to[i] = value_[i];
            }
            to[num] = '\0';
        }

        public char[] getCharacters()
        {
            int num = length();
            char[] array = new char[num + 1];
            getCharacters(array);
            return array;
        }

        public NSString substringWithRange(NSRange range)
        {
            return new NSString(value_.Substring((int)range.location, (int)range.length));
        }

        public NSString substringFromIndex(int n)
        {
            return new NSString(value_[n..]);
        }

        public NSString substringToIndex(int n)
        {
            return new NSString(value_[..n]);
        }

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

        public bool boolValue()
        {
            if (value_.Length == 0)
            {
                return false;
            }
            string text = value_.ToLower();
            return text == "true";
        }

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

        public bool hasPrefix(NSString prefix)
        {
            return value_.StartsWith(prefix.ToString());
        }

        private readonly string value_;
    }
}
