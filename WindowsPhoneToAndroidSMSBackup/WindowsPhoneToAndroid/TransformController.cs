using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace WindowsPhoneToAndroidSMSBackup.WindowsPhoneToAndroid
{
    public class TransformController
    {
        private readonly IExtractWindowsPhone _extractor;
        private readonly IConvertToAndroid _convertToAndroid;

        public TransformController(IExtractWindowsPhone extractor, IConvertToAndroid convertToAndroid)
        {
            _extractor = extractor;
            _convertToAndroid = convertToAndroid;
        }

        public XmlDocument Transform(string bodyFakeBody)
        {
            var messages = _extractor.Extract("<body>fake</body>");
            var xmlDoc = new XmlDocument();
            var smsNode = xmlDoc.CreateElement("smses");
            var smsNodeList = new List<XmlElement>();

            smsNodeList = messages.Select(x => _convertToAndroid.Convert(x)).ToList();
            foreach (var xmlNode in smsNodeList)
            {
                var importedNode = smsNode.OwnerDocument.ImportNode(xmlNode, true);
                smsNode.AppendChild(importedNode);
            }

            smsNode.SetAttribute("count", messages.Count.ToString());
            smsNode.SetAttribute("backup_date", Helpers.ConvertToUnixTimestamp(DateTime.Now).ToString());

            xmlDoc.AppendChild(smsNode);
            return xmlDoc;
        }
    }
}
