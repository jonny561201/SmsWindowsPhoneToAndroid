﻿using System;
using System.Collections.Generic;
using System.Linq;
using WindowsPhoneToAndroidSMSBackup.WindowsPhoneToAndroid;
using WindowsPhoneToAndroidSMSBackup.WindowsPhoneToAndroid.Models;
using NUnit.Framework;

namespace WindowsPhoneToAndroidSMSBackup.WinowsPhoneToAndroid.Tests
{
    [TestFixture]
    public class ExtractWindowsPhoneTests
    {
        public string singleMessage = "<Message><Recepients/><Body>Ooh good to know!! Thanks</Body><IsIncoming>true</IsIncoming><IsRead>true</IsRead><Attachments/><LocalTimestamp>131348483095578379</LocalTimestamp><Sender>5551234567</Sender></Message>";
        public string singleRecipientMessage = "<Message><Recepients><string>5551234567</string></Recepients><Body>Whitey's Ice Cream 😊 </Body><IsIncoming>false</IsIncoming><IsRead>true</IsRead><Attachments /><LocalTimestamp>131348466967150215</LocalTimestamp><Sender/></Message>";
        public string multiMessage = "<ArrayOfMessage><Message><Recepients><string>5551234567</string></Recepients><Body>Whitey's Ice Cream 😊 </Body><IsIncoming>false</IsIncoming><IsRead>true</IsRead><Attachments /><LocalTimestamp>131348466967150215</LocalTimestamp><Sender/></Message><Message><Recepients/><Body>Ooh good to know!! Thanks</Body><IsIncoming>true</IsIncoming><IsRead>true</IsRead><Attachments/><LocalTimestamp>131348483095578379</LocalTimestamp><Sender>5551234567</Sender></Message></ArrayOfMessage>";
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
            var expected = "5551234567";

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
        public void ExtractShouldParseOutIncomingAsFalse()
        {
            var expected = false;
            var actual = ExtractWindows.Extract(singleRecipientMessage);

            Assert.AreEqual(expected, actual.First().IsIncoming);
        }

        [Test]
        public void ExtractShouldParseOutRecipientMessage()
        {
            var expected = "5551234567";
            var actual = ExtractWindows.Extract(singleRecipientMessage);

            Assert.AreEqual(expected, actual.First().Address);
        }

        [Test]
        public void ExtractShouldParseOutMultipleMessages()
        {
            var expectedOneDate = DateTime.FromFileTime(131348483095578379);
            var expectedTwoDate = DateTime.FromFileTime(131348466967150215);
            var exectedOne = new Message("Ooh good to know!! Thanks", "5551234567", expectedOneDate, true, true);
            var exectedTwo = new Message("Whitey's Ice Cream 😊 ", "5551234567", expectedTwoDate, true, true);
            var expectedList = new List<Message> {exectedOne, exectedTwo};
            var actual = ExtractWindows.Extract(multiMessage);

            Assert.AreEqual(2, actual.Count);
            CollectionAssert.AreEquivalent(expectedList, actual);
        }
    }
}