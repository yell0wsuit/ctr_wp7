using System;

namespace ctr_wp7.ios
{
    // Token: 0x020000BE RID: 190
    internal class NSBool : NSObject
    {
        // Token: 0x0600058E RID: 1422 RVA: 0x00029ED4 File Offset: 0x000280D4
        public static NSBool boolWithBool(bool v)
        {
            return new NSBool
            {
                _value = v
            };
        }

        // Token: 0x0600058F RID: 1423 RVA: 0x00029EEF File Offset: 0x000280EF
        public virtual bool boolValue()
        {
            return this._value;
        }

        // Token: 0x04000ADE RID: 2782
        public bool _value;
    }
}
