using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using WindowsPhoneToAndroidSMSBackup.WindowsPhoneToAndroid.Models;

namespace WindowsPhoneToAndroidSMSBackup.WindowsPhoneToAndroid
{
    public interface IExtractWindowsPhone
    {
        List<Message> Extract(string xmlString);
    }

    public class ExtractWindowsPhone : IExtractWindowsPhone
    {
        private const string BodyXpath = "./Body";
        private const string RecipientXpath = "./Recepients/string";
        private const string AddressXpath = "./Address";
        private const string TimestampXpath = "./LocalTimestamp";
        private const string IsReadXpath = "./IsRead";
        private const string IsIncomingXpath = "./IsRead";
        private const string MessageTag = "//Message";

        public List<Message> Extract(string xmlString)
        {
            var xdoc = new XmlDocument();
            xdoc.LoadXml(xmlString);

            var nodes = xdoc.SelectNodes(MessageTag);

            return (from XmlNode node in nodes select ExtractMessage(node)).ToList();
        }

        private static Message ExtractMessage(XmlNode node)
        {
            var timeStamp = DateTime.FromFileTime(long.Parse(node.SelectSingleNode(TimestampXpath).InnerText));
            var body = node.SelectSingleNode(BodyXpath).InnerText;
            var addressNode = node.SelectSingleNode(AddressXpath).InnerText;
            var address = addressNode == ""
                    ? node.SelectSingleNode(RecipientXpath).InnerText
                    : addressNode;
            var isRead = bool.Parse(node.SelectSingleNode(IsReadXpath).InnerText);
            var isIncoming = bool.Parse(node.SelectSingleNode(IsIncomingXpath).InnerText);
            return new Message(body, address, timeStamp, isRead, isIncoming);
        }
    }
}

