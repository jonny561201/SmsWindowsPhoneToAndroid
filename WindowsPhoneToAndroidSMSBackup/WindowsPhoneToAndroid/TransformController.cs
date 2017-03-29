using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void Transform()
        {
            var messages = _extractor.Extract("<body>fake</body>");
            _convertToAndroid.Convert(messages.First());
        }
    }
}
