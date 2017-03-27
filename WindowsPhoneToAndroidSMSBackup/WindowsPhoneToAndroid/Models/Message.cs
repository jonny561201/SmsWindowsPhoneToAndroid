using System;

namespace WindowsPhoneToAndroidSMSBackup.WindowsPhoneToAndroid.Models
{
    public class Message
    {
        public Message(string Body, string Address, DateTime Timestamp, bool IsRead, bool IsIncoming)
        {
            this.Body = Body;
            this.Address = Address;
            TimeStamp = Timestamp;
            this.IsRead = IsRead;
            this.IsIncoming = IsIncoming;
        }

        public string Body { get; set; }
        public string Address { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool IsRead { get; set; }
        public bool IsIncoming { get; set; }

        public override bool Equals(object obj)
        {
            var message = obj as Message;
            return (message.Body == Body &&
                    message.Address == Address &&
                    message.TimeStamp == TimeStamp &&
                    message.IsIncoming == IsIncoming &&
                    message.IsRead == IsRead);
        }

        public override int GetHashCode()
        {
            return Body.Length + int.Parse(Address);
        }
    }
}
