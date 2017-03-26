using System;

namespace WindowsPhoneToAndroidSMSBackup.WindowsPhoneToAndroid.Models
{
    public class Message
    {
        public string Body { get; set; }
        public string Sender { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
