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

        public XmlDocument Transform()
        {
            var messages = _extractor.Extract("<body>fake</body>");
            var xmlDoc = new XmlDocument();
            var smsNode = xmlDoc.CreateElement("smses");
            var smsNodeList = new List<XmlNode>();

            messages.Select(x => _convertToAndroid.Convert(x)).ToList();

            smsNode.SetAttribute("count", messages.Count.ToString());

            xmlDoc.AppendChild(smsNode);
            return xmlDoc;
        }
    }
}
