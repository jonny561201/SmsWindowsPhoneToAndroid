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
            var message = new Message("FakeBody", "5551234567", DateTime.Now, true, true);
            var expectedMessages = new List<Message> { message };
            _extractor.Setup(x => x.Extract(FakeXml)).Returns(expectedMessages);

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

        [Test]
        public void TransformShouldIterateOverMessagesFromExtractToConverter()
        {
            var message1 = new Message("FakeBody1", "5551234567", DateTime.Now, true, true);
            var message2 = new Message("FakeBody2", "5551234568", DateTime.Now, false, false);
            var expectedMessages = new List<Message> {message1, message2};
            _extractor.Setup(x => x.Extract(FakeXml)).Returns(expectedMessages);
            _controller.Transform();

            _converter.Verify(x => x.Convert(message1));
            _converter.Verify(x => x.Convert(message2));
        }

        [Test]
        public void TransformShouldReturnXmlDocumentWithSmsesTag()
        {
            var message = new Message("FakeBody1", "5551234567", DateTime.Now, true, true);
            var expectedMessages = new List<Message> { message };
            _extractor.Setup(x => x.Extract(FakeXml)).Returns(expectedMessages);
            var converter = new ConvertToAndroid();
            var controller = new TransformController(_extractor.Object, converter);

            var actual = controller.Transform();

            Assert.AreEqual("smses", actual.FirstChild.Name);
        }

        [Test]
        public void TransformShouldReturnXmlDocumentWithCountAttribute()
        {
            var message = new Message("FakeBody1", "5551234567", DateTime.Now, true, true);
            var expectedMessages = new List<Message> { message };
            _extractor.Setup(x => x.Extract(FakeXml)).Returns(expectedMessages);
            var converter = new ConvertToAndroid();
            var controller = new TransformController(_extractor.Object, converter);

            var actual = controller.Transform();

            Assert.AreEqual("1", actual.FirstChild.Attributes["count"].Value);
        }
    }
}
