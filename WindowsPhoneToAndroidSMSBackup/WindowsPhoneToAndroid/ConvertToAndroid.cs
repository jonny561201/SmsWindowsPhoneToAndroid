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
            var longDateString = ConvertToUnixTimestamp(message.TimeStamp);
            smsNode.SetAttribute("date", longDateString.ToString());
            xmlDoc.AppendChild(smsNode);
            
            return xmlDoc;
        }
        public static double ConvertToUnixTimestamp(DateTime date)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalMilliseconds);
        }
    }
}

//    <sms
//        protocol = "0"
//        address="5153138947"
//        date="1490545459777"
//        type="1"
//        subject="null"
//        body="Facebook - we are in the running to win best breakfast!! Need people "
//        toa="null"
//        sc_toa="null"
//        service_center="null"
//        read="1"
//        status="-1"
//        locked="0"
//        date_sent="1490545375000"
//        readable_date="Mar 26, 2017 11:24:19 AM"
//        contact_name="(Unknown)"/>