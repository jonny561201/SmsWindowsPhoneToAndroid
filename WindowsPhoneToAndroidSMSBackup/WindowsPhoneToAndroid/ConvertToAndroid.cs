using System;
using System.Xml;
using WindowsPhoneToAndroidSMSBackup.WindowsPhoneToAndroid.Models;

namespace WindowsPhoneToAndroidSMSBackup.WindowsPhoneToAndroid
{
    public class ConvertToAndroid
    {
        public XmlNode Convert(Message message)
        {
            var xmlDoc = new XmlDocument();

            var smsNode = xmlDoc.CreateElement("sms");
            smsNode.SetAttribute("body", message.Body);
            smsNode.SetAttribute("address", message.Address);
            smsNode.SetAttribute("type", ConvertToType(message.IsIncoming));
            smsNode.SetAttribute("read", System.Convert.ToInt32(message.IsRead).ToString());
            smsNode.SetAttribute("readable_date", message.TimeStamp.ToString("MMM dd, yyyy hh:mm:ss tt"));
            smsNode.SetAttribute("date", ConvertToUnixTimestamp(message.TimeStamp).ToString());
            xmlDoc.AppendChild(smsNode);
            
            return xmlDoc;
        }

        private static string ConvertToType(bool isIncoming)
        {
            return isIncoming ? "1" : "2";
        }

        private static double ConvertToUnixTimestamp(DateTime date)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalMilliseconds);
        }
    }
}

//    <sms
//        protocol = "0"
//        type="1"
//        subject="null"
//        toa="null"
//        sc_toa="null"
//        service_center="null"
//        status="-1"
//        locked="0"
//        date_sent="1490545375000"
//        contact_name="(Unknown)"/>