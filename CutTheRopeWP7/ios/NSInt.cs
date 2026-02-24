using System;

namespace ctr_wp7.ios
{
    // Token: 0x020000BC RID: 188
    internal class NSInt : NSObject
    {
        // Token: 0x06000588 RID: 1416 RVA: 0x00029E7C File Offset: 0x0002807C
        public static NSInt intWithInt(int v)
        {
            return new NSInt
            {
                _value = v
            };
        }

        // Token: 0x06000589 RID: 1417 RVA: 0x00029E97 File Offset: 0x00028097
        public virtual int intValue()
        {
            return this._value;
        }

        // Token: 0x04000ADC RID: 2780
        public int _value;
    }
}
