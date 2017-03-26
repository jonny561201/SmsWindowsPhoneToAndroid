using NUnit.Framework;

namespace WindowsPhoneToAndroidSMSBackup.WinowsPhoneToAndroid.Tests
{
    [TestFixture]
    public class ExtractWindowsPhoneTests
    {
        public string Message = "<Message><Recepients/>< Body > Ooh good to know!! Thanks</Body><IsIncoming>true</IsIncoming><IsRead>true</IsRead><Attachments/><LocalTimestamp>131348483095578379</LocalTimestamp><Sender>5153138947</Sender></Message>";
        public ExtractWindowsPhone extractWindows;

        [SetUp]
        public void Setup()
        {
            
        }

    }
}

//<Message><Recepients/>
//    <Body>Ooh good to know!! Thanks
//    </Body>
//    <IsIncoming>true</IsIncoming>
//    <IsRead>true</IsRead><Attachments/>
//    <LocalTimestamp>131348483095578379</LocalTimestamp>
//    <Sender>5153138947</Sender>
//</Message>