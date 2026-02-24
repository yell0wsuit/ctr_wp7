namespace ctr_wp7.ios
{
    internal sealed class NSBool : NSObject
    {
        public static NSBool boolWithBool(bool v)
        {
            return new NSBool
            {
                _value = v
            };
        }

        public bool boolValue()
        {
            return _value;
        }

        public bool _value;
    }
}
