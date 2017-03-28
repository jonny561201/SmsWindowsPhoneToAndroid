using System;
using WindowsPhoneToAndroidSMSBackup.WindowsPhoneToAndroid;
using WindowsPhoneToAndroidSMSBackup.WindowsPhoneToAndroid.Models;
using NUnit.Framework;

namespace WindowsPhoneToAndroidSMSBackup.WinowsPhoneToAndroid.Tests
{
    [TestFixture]
    public class ConvertToAndroidTests
    {
        private ConvertToAndroid convertAndroid;
        private Message message;

        [SetUp]
        public void Setup()
        {
            convertAndroid = new ConvertToAndroid();
            message = new Message("TestBody", "5551234567", DateTime.FromFileTime(131348483095578379), true, true);
        }

        [Test]
        public void ConvertShouldTransformBodyToAndroidAttribute()
        {
            var actual = convertAndroid.Convert(message);

            var result = actual.SelectSingleNode("./sms").Attributes["body"];

            Assert.AreEqual("TestBody", result.Value);
        }

        [Test]
        public void ConvertShouldTransformAddressToAndroidAttribute()
        {
            var actual = convertAndroid.Convert(message);

            var result = actual.SelectSingleNode("./sms").Attributes["address"];

            Assert.AreEqual("5551234567", result.Value);
        }

        [Test]
        public void ConvertShouldTransformDateToAndroidAttribute()
        {
            var actual = convertAndroid.Convert(message);

            var result = actual.SelectSingleNode("./sms").Attributes["date"];

            Assert.AreEqual("1490374709557", result.Value);
        }
    }
}
