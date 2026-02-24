namespace ctr_wp7.ios
{
    internal sealed class NSFloat : NSObject
    {
        public static NSFloat floatWithFloat(float v)
        {
            return new NSFloat
            {
                _value = v
            };
        }

        public float floatValue()
        {
            return _value;
        }

        public float _value;
    }
}
