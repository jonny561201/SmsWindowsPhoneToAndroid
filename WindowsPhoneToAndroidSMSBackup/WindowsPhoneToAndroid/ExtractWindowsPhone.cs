using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using WindowsPhoneToAndroidSMSBackup.WindowsPhoneToAndroid.Models;

namespace WindowsPhoneToAndroidSMSBackup.WindowsPhoneToAndroid
{
    public class ExtractWindowsPhone
    {
        private readonly string MessageTag = "//Message";

        public Message Extract(string xmlString)
        {
            var xdoc = new XmlDocument();

            xdoc.LoadXml(xmlString);

            var nodes = xdoc.SelectNodes(MessageTag);
            var message = new Message();
            var bodyXpath = "//Body";
            var sendXpath = "//Sender";
            var timestampXpath = "//LocalTimestamp";
            var isReadXpath = "//IsRead";
            var isIncomingXpath = "//IsRead";

            var timeStamp = long.Parse(nodes.Item(0).SelectSingleNode(timestampXpath).InnerText);
            message.Body = nodes.Item(0).SelectSingleNode(bodyXpath).InnerText;
            message.Sender = nodes.Item(0).SelectSingleNode(sendXpath).InnerText;
            message.IsRead = bool.Parse(nodes.Item(0).SelectSingleNode(isReadXpath).InnerText);
            message.IsIncoming = bool.Parse(nodes.Item(0).SelectSingleNode(isIncomingXpath).InnerText);
            message.TimeStamp = DateTime.FromFileTime(timeStamp);

            return message;
        }
    }
}
