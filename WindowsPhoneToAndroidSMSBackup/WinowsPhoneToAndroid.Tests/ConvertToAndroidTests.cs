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
            message = new Message("TestBody", "5551234567", DateTime.Now, true, true);
        }

        [Test]
        public void ConvertShouldTransformBodyToAndroidAttribute()
        {
            var actual = convertAndroid.Convert(message);

            var result = actual.SelectSingleNode("./sms").Attributes["body"];

            Assert.AreEqual(result.Value, "TestBody");
        }

        [Test]
        public void ConvertShouldTransformAddressToAndroidAttribute()
        {
            var actual = convertAndroid.Convert(message);

            var result = actual.SelectSingleNode("./sms").Attributes["address"];

            Assert.AreEqual(result.Value, "5551234567");
        }
    }
}
