using System.Xml;
using WindowsPhoneToAndroidSMSBackup.WindowsPhoneToAndroid.Models;

namespace WindowsPhoneToAndroidSMSBackup.WindowsPhoneToAndroid
{
    public class ExtractWindowsPhone
    {
        private readonly string MessageNodes = "//Message";

        public Message Extract(string xmlString)
        {
            var xdoc = new XmlDocument();

            xdoc.LoadXml(xmlString);

            var nodes = xdoc.SelectNodes(MessageNodes);
            var message = new Message();
            var bodyXpath = "//Body";
            var sendXpath = "//Sender";
            message.Body = nodes.Item(0).SelectSingleNode(bodyXpath).InnerText;
            message.Sender = nodes.Item(0).SelectSingleNode(sendXpath).InnerText;

            return message;
        }
    }
}
