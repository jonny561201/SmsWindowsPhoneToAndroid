﻿using System;
using System.Collections.Generic;
using System.Xml;
using WindowsPhoneToAndroidSMSBackup.WindowsPhoneToAndroid.Models;

namespace WindowsPhoneToAndroidSMSBackup.WindowsPhoneToAndroid
{
    public class ExtractWindowsPhone
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
            var messages = new List<Message>();

            foreach (XmlNode node in nodes)
                messages.Add(ExtractMessage(node, messages));

            return messages;
        }

        private static Message ExtractMessage(XmlNode node, ICollection<Message> messages)
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

