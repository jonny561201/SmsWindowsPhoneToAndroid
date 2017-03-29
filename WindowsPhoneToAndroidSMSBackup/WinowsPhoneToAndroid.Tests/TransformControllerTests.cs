using System;
using System.Collections.Generic;
using System.Linq;
using WindowsPhoneToAndroidSMSBackup.WindowsPhoneToAndroid;
using WindowsPhoneToAndroidSMSBackup.WindowsPhoneToAndroid.Models;
using Moq;
using NUnit.Framework;

namespace WindowsPhoneToAndroidSMSBackup.WinowsPhoneToAndroid.Tests
{
    [TestFixture]
    public class TransformControllerTests
    {
        private TransformController _controller;
        private Mock<IExtractWindowsPhone> _extractor;
        private Mock<IConvertToAndroid> _converter;
        private const string FakeXml = "<body>fake</body>";

        [SetUp]
        public void Setup()
        {
            _converter = new Mock<IConvertToAndroid>();
            _extractor = new Mock<IExtractWindowsPhone>();
            _controller = new TransformController(_extractor.Object, _converter.Object);
        }

        [Test]
        public void TransformShouldCallExtract()
        {
            _controller.Transform();

            _extractor.Verify(x => x.Extract(FakeXml));
        }

        [Test]
        public void TransformShouldPassResultFromExtractToConverter()
        {
            var message = new Message("FakeBody", "5551234567", DateTime.Now, true, true);
            var expectedMessages = new List<Message> {message};
            _extractor.Setup(x => x.Extract(FakeXml)).Returns(expectedMessages);
            _controller.Transform();

            _converter.Verify(x => x.Convert(expectedMessages.First()));
        }
    }
}
