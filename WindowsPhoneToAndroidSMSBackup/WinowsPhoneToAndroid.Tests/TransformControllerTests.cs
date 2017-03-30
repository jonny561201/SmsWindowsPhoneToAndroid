using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
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
        private XmlElement _element;

        [SetUp]
        public void Setup()
        {
            _element = new XmlDocument().CreateElement("sms");
            _converter = new Mock<IConvertToAndroid>();
            _extractor = new Mock<IExtractWindowsPhone>();
            _controller = new TransformController(_extractor.Object, _converter.Object);
        }

        [Test]
        public void TransformShouldCallExtract()
        {
            var message = new Message("FakeBody", "555123456", DateTime.Now, true, true);
            var expectedMessages = new List<Message> { message };
            _extractor.Setup(x => x.Extract(FakeXml)).Returns(expectedMessages);
            _converter.Setup(x => x.Convert(message)).Returns(_element);

            _controller.Transform(FakeXml);

            _extractor.Verify(x => x.Extract(FakeXml));
        }

        [Test]
        public void TransformShouldCallExtractWithSuppliedString()
        {
            var altXmlString = "<fakeElement/>";
            var message = new Message("FakeBody", "555123456", DateTime.Now, true, true);
            var expectedMessages = new List<Message> { message };
            _extractor.Setup(x => x.Extract(altXmlString)).Returns(expectedMessages);
            _converter.Setup(x => x.Convert(message)).Returns(_element);

            _controller.Transform(altXmlString);

            _extractor.Verify(x => x.Extract(altXmlString));
        }

        [Test]
        public void TransformShouldPassResultFromExtractToConverter()
        {
            var message = new Message("FakeBody", "555123456", DateTime.Now, true, true);
            var expectedMessages = new List<Message> {message};
            _extractor.Setup(x => x.Extract(FakeXml)).Returns(expectedMessages);
            _converter.Setup(x => x.Convert(message)).Returns(_element);
            _controller.Transform(FakeXml);

            _converter.Verify(x => x.Convert(expectedMessages.First()));
        }

        [Test]
        public void TransformShouldIterateOverMessagesFromExtractToConverter()
        {
            var message1 = new Message("FakeBody1", "555123456", DateTime.Now, true, true);
            var message2 = new Message("FakeBody2", "555123456", DateTime.Now, false, false);
            var expectedMessages = new List<Message> {message1, message2};
            _extractor.Setup(x => x.Extract(FakeXml)).Returns(expectedMessages);
            _converter.Setup(x => x.Convert(It.IsAny<Message>())).Returns(_element);
            _controller.Transform(FakeXml);

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

            var actual = controller.Transform(FakeXml);

            Assert.AreEqual("smses", actual.LastChild.Name);
        }

        [Test]
        public void TransformShouldReturnXmlDocumentWithAttributes()
        {
            var message = new Message("FakeBody1", "5551234567", DateTime.Now, true, true);
            var expectedMessages = new List<Message> { message };
            _extractor.Setup(x => x.Extract(FakeXml)).Returns(expectedMessages);
            var converter = new ConvertToAndroid();
            var controller = new TransformController(_extractor.Object, converter);

            var actual = controller.Transform(FakeXml);

            Assert.AreEqual("1", actual.LastChild.Attributes["count"].Value);
            Assert.AreEqual("backup_date", actual.LastChild.Attributes["backup_date"].Name);
        }

        [Test]
        public void TransformShouldReturnXmlDocumentWithSmsElement()
        {
            var message = new Message("FakeBody1", "5551234567", DateTime.Now, true, true);
            var expectedMessages = new List<Message> { message };
            _extractor.Setup(x => x.Extract(FakeXml)).Returns(expectedMessages);
            var converter = new ConvertToAndroid();
            var controller = new TransformController(_extractor.Object, converter);

            var actual = controller.Transform(FakeXml);

            Assert.AreEqual("sms", actual.LastChild.FirstChild.Name);
        }

        [Test]
        public void TransformShouldReturnXmlDocumentWithMultipleSmsElement()
        {
            var message1 = new Message("FakeBody1", "5551234567", DateTime.Now, true, true);
            var message2 = new Message("FakeBody2", "5551234567", DateTime.Now, true, true);
            var expectedMessages = new List<Message> { message1, message2 };
            _extractor.Setup(x => x.Extract(FakeXml)).Returns(expectedMessages);
            var converter = new ConvertToAndroid();
            var controller = new TransformController(_extractor.Object, converter);

            var actual = controller.Transform(FakeXml);

            Assert.AreEqual(2, actual.LastChild.ChildNodes.Count);
            Assert.AreEqual("sms", actual.LastChild.FirstChild.Name);
            Assert.AreEqual("sms", actual.LastChild.LastChild.Name);
        }

        [Test]
        public void TransformShouldReturnXmlDocumentWithDeclarationTag()
        {
            var message = new Message("FakeBody1", "5551234567", DateTime.Now, true, true);
            var expectedMessages = new List<Message> { message };
            _extractor.Setup(x => x.Extract(FakeXml)).Returns(expectedMessages);
            var converter = new ConvertToAndroid();
            var controller = new TransformController(_extractor.Object, converter);

            var actual = controller.Transform(FakeXml);

            Assert.AreEqual("xml", actual.ChildNodes[0].Name);
        }
    }
}
