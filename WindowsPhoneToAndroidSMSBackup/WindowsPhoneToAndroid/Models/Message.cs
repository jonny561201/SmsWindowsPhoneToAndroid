﻿using System;

namespace WindowsPhoneToAndroidSMSBackup.WindowsPhoneToAndroid.Models
{
    public class Message
    {
        public string Body { get; set; }
        public string Sender { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool IsRead { get; set; }
        public bool IsIncoming { get; set; }
    }
}
