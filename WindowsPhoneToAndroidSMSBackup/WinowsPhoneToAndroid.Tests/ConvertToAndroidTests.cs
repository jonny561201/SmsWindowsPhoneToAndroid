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
            message = new Message("TestBody", "5153138947", DateTime.Now, true, true);
        }

        [Test]
        public void ConvertShouldTransformBodyToAndroidString()
        {
            var actual = convertAndroid.Convert(message);

            var result = actual.SelectSingleNode("//sms[@body]").Attributes;

            Assert.AreEqual(result[0].Value, "TestBody");
        }
    }
}
