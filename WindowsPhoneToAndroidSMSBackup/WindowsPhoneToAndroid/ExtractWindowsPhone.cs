using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using WindowsPhoneToAndroidSMSBackup.WindowsPhoneToAndroid.Models;
using NUnit.Framework.Constraints;

namespace WindowsPhoneToAndroidSMSBackup.WindowsPhoneToAndroid
{
    public class ExtractWindowsPhone
    {
        private const string BodyXpath = "//Body";
        private const string SendXpath = "//Sender";
        private const string RecipientXpath = "//Recepients/string";
        private const string AddressXpath = "//Address";
        private const string TimestampXpath = "//LocalTimestamp";
        private const string IsReadXpath = "//IsRead";
        private const string IsIncomingXpath = "//IsRead";
        private readonly string MessageTag = "//Message";

        public List<Message> Extract(string xmlString)
        {
            var xdoc = new XmlDocument();
            xdoc.LoadXml(xmlString);

            var nodes = xdoc.SelectNodes(MessageTag);
            var messages = new List<Message>();

            ExtractMessages(nodes, messages);

            return messages;
        }

        private static void ExtractMessages(XmlNodeList nodes, List<Message> messages)
        {
            foreach (XmlNode node in nodes)
            {
                var message = new Message();
                var timeStamp = long.Parse(node.SelectSingleNode(TimestampXpath).InnerText);
                message.Body = node.SelectSingleNode(BodyXpath).InnerText;
                var selectSingleNode = node.SelectSingleNode(AddressXpath);
                message.Address = selectSingleNode.InnerText == ""
                    ? node.SelectSingleNode(RecipientXpath).InnerText
                    : selectSingleNode.InnerText;
                message.IsRead = bool.Parse(node.SelectSingleNode(IsReadXpath).InnerText);
                message.IsIncoming = bool.Parse(node.SelectSingleNode(IsIncomingXpath).InnerText);
                message.TimeStamp = DateTime.FromFileTime(timeStamp);
                messages.Add(message);
            }
        }
    }
}

