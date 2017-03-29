using System;

namespace WindowsPhoneToAndroidSMSBackup.WindowsPhoneToAndroid
{
    public class Helpers
    {
        public static double ConvertToUnixTimestamp(DateTime date)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalMilliseconds);
        }
    }
}