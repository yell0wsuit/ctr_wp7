namespace ctr_wp7.ios
{
    internal sealed class NSInt : NSObject
    {
        public static NSInt intWithInt(int v)
        {
            return new NSInt
            {
                _value = v
            };
        }

        public int intValue()
        {
            return _value;
        }

        public int _value;
    }
}
