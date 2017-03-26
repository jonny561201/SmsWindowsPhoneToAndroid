using WindowsPhoneToAndroidSMSBackup.WindowsPhoneToAndroid;
using NUnit.Framework;

namespace WindowsPhoneToAndroidSMSBackup.WinowsPhoneToAndroid.Tests
{
    [TestFixture]
    public class ExtractWindowsPhoneTests
    {
        public string Message = "<Message><Recepients/><Body>Ooh good to know!! Thanks</Body><IsIncoming>true</IsIncoming><IsRead>true</IsRead><Attachments/><LocalTimestamp>131348483095578379</LocalTimestamp><Sender>5153138947</Sender></Message>";
        public ExtractWindowsPhone ExtractWindows;

        [SetUp]
        public void Setup()
        {
            ExtractWindows = new ExtractWindowsPhone();   
        }

        [Test]
        public void ExtractShouldParseOutMessageBody()
        {
            var actual = ExtractWindows.Extract(Message);
            var expected = "Ooh good to know!! Thanks";

            Assert.AreEqual(expected, actual.body);
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