using System.Linq;

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
            foreach (var message in messages)
            {
                _convertToAndroid.Convert(message);
            }
        }
    }
}
