﻿using System;
using System.Linq;
using WindowsPhoneToAndroidSMSBackup.WindowsPhoneToAndroid;
using WindowsPhoneToAndroidSMSBackup.WindowsPhoneToAndroid.Models;
using NUnit.Framework;

namespace WindowsPhoneToAndroidSMSBackup.WinowsPhoneToAndroid.Tests
{
    [TestFixture]
    public class ExtractWindowsPhoneTests
    {
        public string singleMessage = "<Message><Recepients/><Body>Ooh good to know!! Thanks</Body><IsIncoming>true</IsIncoming><IsRead>true</IsRead><Attachments/><LocalTimestamp>131348483095578379</LocalTimestamp><Address>5153138947</Address></Message>";
        public string singleRecipientMessage = "<Message><Recepients><string>5153138947</string></Recepients><Body>Whitey's Ice Cream 😊 </Body><IsIncoming>false</IsIncoming><IsRead>true</IsRead><Attachments /><LocalTimestamp>131348466967150215</LocalTimestamp><Address/></Message>";
        public string multiMessage = "<ArrayOfMessage><Message><Recepients><string>5153138947</string></Recepients><Body>Whitey's Ice Cream 😊 </Body><IsIncoming>false</IsIncoming><IsRead>true</IsRead><Attachments /><LocalTimestamp>131348466967150215</LocalTimestamp><Address/></Message><Message><Recepients/><Body>Ooh good to know!! Thanks</Body><IsIncoming>true</IsIncoming><IsRead>true</IsRead><Attachments/><LocalTimestamp>131348483095578379</LocalTimestamp><Address>5153138947</Address></Message></ArrayOfMessage>";
        public ExtractWindowsPhone ExtractWindows;

        [SetUp]
        public void Setup()
        {
            ExtractWindows = new ExtractWindowsPhone();   
        }

        [Test]
        public void ExtractShouldParseOutMessageBody()
        {
            var actual = ExtractWindows.Extract(singleMessage);
            var expected = "Ooh good to know!! Thanks";

            Assert.AreEqual(expected, actual.First().Body);
        }

        [Test]
        public void ExtractShouldParseOutMessageSender()
        {
            var actual = ExtractWindows.Extract(singleMessage);
            var expected = "5153138947";

            Assert.AreEqual(expected, actual.First().Address);
        }

        [Test]
        public void ExtractShouldParseOutLocalTimeStamp()
        {
            var expected = DateTime.FromFileTime(131348483095578379);
            var actual = ExtractWindows.Extract(singleMessage);

            Assert.AreEqual(expected, actual.First().TimeStamp);
        }

        [Test]
        public void ExtractShouldParseOutIsReadValue()
        {
            var expected = true;
            var actual = ExtractWindows.Extract(singleMessage);

            Assert.AreEqual(expected, actual.First().IsRead);
        }

        [Test]
        public void ExtractShouldParseOutIncoming()
        {
            var expected = true;
            var actual = ExtractWindows.Extract(singleMessage);

            Assert.AreEqual(expected, actual.First().IsIncoming);
        }

        [Test]
        public void ExtractShouldParseOutRecipientMessage()
        {
            var expected = "5153138947";
            var actual = ExtractWindows.Extract(singleRecipientMessage);

            Assert.AreEqual(expected, actual.First().Address);
        }

        [Test]
        public void ExtractShouldParseOutMultipleMessages()
        {
            var exected1 = new Message();
            var exected2 = new Message();
            var actual = ExtractWindows.Extract(multiMessage);

            Assert.AreEqual(2, actual.Count);
            Assert.Contains(exected1, actual);
            Assert.Contains(exected2, actual);
        }
    }
}

//<singleMessage><Recepients/>
//    <Body>Ooh good to know!! Thanks
//    </Body>
//    <IsIncoming>true</IsIncoming>
//    <IsRead>true</IsRead><Attachments/>
//    <LocalTimestamp>131348483095578379</LocalTimestamp>
//    <Address>5153138947</Address>
//</singleMessage>