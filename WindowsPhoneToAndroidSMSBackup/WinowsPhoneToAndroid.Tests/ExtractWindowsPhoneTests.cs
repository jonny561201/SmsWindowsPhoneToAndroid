﻿using System;
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

            Assert.AreEqual(expected, actual.Body);
        }

        [Test]
        public void ExtractShouldParseOutMessageSender()
        {
            var actual = ExtractWindows.Extract(Message);
            var expected = "5153138947";

            Assert.AreEqual(expected, actual.Sender);
        }

        [Test]
        public void ExtractShouldParseOutLocalTimeStamp()
        {
            var expected = DateTime.FromFileTime(131348483095578379);
            var actual = ExtractWindows.Extract(Message);

            Assert.AreEqual(expected, actual.TimeStamp);
        }

        [Test]
        public void ExtractShouldParseOutIsReadValue()
        {
            var expected = true;
            var actual = ExtractWindows.Extract(Message);

            Assert.AreEqual(expected, actual.IsRead);
        }

        [Test]
        public void ExtractShouldParseOutIncoming()
        {
            var expected = true;
            var actual = ExtractWindows.Extract(Message);

            Assert.AreEqual(expected, actual.IsIncoming);
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