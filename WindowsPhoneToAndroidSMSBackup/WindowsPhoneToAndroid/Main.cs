using System.IO;
using System.Text;

namespace WindowsPhoneToAndroidSMSBackup.WindowsPhoneToAndroid
{
    public class Main
    {
        private const string WinPhoXmlFilePath = "C:\\Users\\Inuyasha\\Desktop\\smsBackup\\Messages_Backup.xml";
        private const string AndroidXmlSavePath = "C:\\Users\\Inuyasha\\Desktop\\smsBackup\\Messages_Backup_new.xml";

        public void Execute()
        {
            var extractor = new ExtractWindowsPhone();
            var converter = new ConvertToAndroid();
            var controller = new TransformController(extractor, converter);
            string windowsPhoneXml;
            using (var streamReader = new StreamReader(WinPhoXmlFilePath, Encoding.UTF8))
            {
                windowsPhoneXml = streamReader.ReadToEnd();
            }

            var xmlDoc = controller.Transform(windowsPhoneXml);
            xmlDoc.Save(AndroidXmlSavePath);
        }
    }
}
