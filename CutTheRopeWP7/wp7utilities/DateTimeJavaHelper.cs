using System;

namespace ctr_wp7.wp7utilities
{
    // Token: 0x02000042 RID: 66
    public static class DateTimeJavaHelper
    {
        // Token: 0x06000239 RID: 569 RVA: 0x0000EEE0 File Offset: 0x0000D0E0
        public static long currentTimeMillis()
        {
            return (long)(DateTime.UtcNow - DateTimeJavaHelper.Jan1st1970).TotalMilliseconds;
        }

        // Token: 0x0400082A RID: 2090
        private static readonly DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, 1);
    }
}
