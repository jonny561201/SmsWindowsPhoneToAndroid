﻿using System;
using System.Xml;
using WindowsPhoneToAndroidSMSBackup.WindowsPhoneToAndroid.Models;

namespace WindowsPhoneToAndroidSMSBackup.WindowsPhoneToAndroid
{
    public interface IConvertToAndroid
    {
        XmlNode Convert(Message message);
    }

    public class ConvertToAndroid : IConvertToAndroid
    {
        //now need to iterate over list of Messages in controller class that will read file 
        //parse objects and build smses xmlNode containing messages
        public XmlNode Convert(Message message)
        {
            var xmlDoc = new XmlDocument();

            var smsNode = xmlDoc.CreateElement("sms");
            smsNode.SetAttribute("body", message.Body);
            smsNode.SetAttribute("protocol", "0");
            smsNode.SetAttribute("subject", "null");
            smsNode.SetAttribute("toa", "null");
            smsNode.SetAttribute("sc_toa", "null");
            smsNode.SetAttribute("status", "-1");
            smsNode.SetAttribute("locked", "0");
            smsNode.SetAttribute("service_center", "null");
            smsNode.SetAttribute("contact_name", "(Unknown)");
            smsNode.SetAttribute("address", message.Address);
            smsNode.SetAttribute("type", ConvertToType(message.IsIncoming));
            smsNode.SetAttribute("read", System.Convert.ToInt32(message.IsRead).ToString());
            smsNode.SetAttribute("readable_date", message.TimeStamp.ToString("MMM dd, yyyy hh:mm:ss tt"));
            smsNode.SetAttribute("date", ConvertToUnixTimestamp(message.TimeStamp).ToString());
            smsNode.SetAttribute("date_sent", ConvertToUnixTimestamp(message.TimeStamp.AddMinutes(-2)).ToString());
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