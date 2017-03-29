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

        public TransformController(IExtractWindowsPhone extractor)
        {
            _extractor = extractor;
        }

        public void Transform()
        {
            _extractor.Extract("<body>fake</body>");
        }
    }
}
