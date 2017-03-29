using WindowsPhoneToAndroidSMSBackup.WindowsPhoneToAndroid;
using Moq;
using NUnit.Framework;

namespace WindowsPhoneToAndroidSMSBackup.WinowsPhoneToAndroid.Tests
{
    [TestFixture]
    public class TransformControllerTests
    {
        private TransformController controller;
        private Mock<IExtractWindowsPhone> extractor;
        private string FakeXml = "<body>fake</body>";

        [SetUp]
        public void Setup()
        {
            extractor = new Mock<IExtractWindowsPhone>();
            controller = new TransformController(extractor.Object);
        }

        [Test]
        public void TransformShouldCallExtract()
        {
            controller.Transform();

            extractor.Verify(x => x.Extract(FakeXml));
        }
    }
}
