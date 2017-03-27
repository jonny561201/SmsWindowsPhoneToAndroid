using System;

namespace WindowsPhoneToAndroidSMSBackup.WindowsPhoneToAndroid.Models
{
    public class Message
    {
        public Message(string Body, string Address, DateTime Timestamp, bool IsRead, bool IsIncoming)
        {
            this.Body = Body;
            this.Address = Address;
            this.TimeStamp = Timestamp;
            this.IsRead = IsRead;
            this.IsIncoming = IsIncoming;
        }

        public Message()
        {
        }

        public string Body { get; set; }
        public string Address { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool IsRead { get; set; }
        public bool IsIncoming { get; set; }
    }
}
