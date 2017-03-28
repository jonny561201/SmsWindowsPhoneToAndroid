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

        [Test]
        public void ConvertShouldTransformReadableDateToAndroidAttribute()
        {
            var actual = convertAndroid.Convert(message);

            var result = actual.SelectSingleNode("./sms").Attributes["readable_date"];

            Assert.AreEqual("Mar 24, 2017 11:58:29 AM", result.Value);
        }

        [Test]
        public void ConvertShouldTransformReadStatusToAndroidAttribute()
        {
            var actual = convertAndroid.Convert(message);

            var result = actual.SelectSingleNode("./sms").Attributes["read"];

            Assert.AreEqual("1", result.Value);
        }

        [Test]
        public void ConvertShouldTransformIsIncomingToAndroidAttribute()
        {
            var actual = convertAndroid.Convert(message);

            var result = actual.SelectSingleNode("./sms").Attributes["type"];

            Assert.AreEqual("1", result.Value);
        }

        [Test]
        public void ConvertShouldTransformProtocolDefaultedToAndroidAttribute()
        {
            var actual = convertAndroid.Convert(message);

            var result = actual.SelectSingleNode("./sms").Attributes["protocol"];

            Assert.AreEqual("0", result.Value);
        }

        [Test]
        public void ConvertShouldTransformSubjectDefaultedToAndroidAttribute()
        {
            var actual = convertAndroid.Convert(message);

            var result = actual.SelectSingleNode("./sms").Attributes["subject"];

            Assert.AreEqual("null", result.Value);
        }

        [Test]
        public void ConvertShouldTransformToaDefaultedToAndroidAttribute()
        {
            var actual = convertAndroid.Convert(message);

            var result = actual.SelectSingleNode("./sms").Attributes["toa"];

            Assert.AreEqual("null", result.Value);
        }

        [Test]
        public void ConvertShouldTransformScToaDefaultedToAndroidAttribute()
        {
            var actual = convertAndroid.Convert(message);

            var result = actual.SelectSingleNode("./sms").Attributes["sc_toa"];

            Assert.AreEqual("null", result.Value);
        }

        [Test]
        public void ConvertShouldTransformServiceCenterDefaultedToAndroidAttribute()
        {
            var actual = convertAndroid.Convert(message);

            var result = actual.SelectSingleNode("./sms").Attributes["service_center"];

            Assert.AreEqual("null", result.Value);
        }

        [Test]
        public void ConvertShouldTransformStatusDefaultedToAndroidAttribute()
        {
            var actual = convertAndroid.Convert(message);

            var result = actual.SelectSingleNode("./sms").Attributes["status"];

            Assert.AreEqual("-1", result.Value);
        }

        [Test]
        public void ConvertShouldTransformLockedDefaultedToAndroidAttribute()
        {
            var actual = convertAndroid.Convert(message);

            var result = actual.SelectSingleNode("./sms").Attributes["locked"];

            Assert.AreEqual("0", result.Value);
        }

        [Test]
        public void ConvertShouldTransformReducesTimestampDefaultedToAndroidAttribute()
        {
            var actual = convertAndroid.Convert(message);

            var result = actual.SelectSingleNode("./sms").Attributes["date_sent"];

            Assert.AreEqual("1490374589557", result.Value);
        }

        [Test]
        public void ConvertShouldTransformContactNameDefaultedToAndroidAttribute()
        {
            var actual = convertAndroid.Convert(message);

            var result = actual.SelectSingleNode("./sms").Attributes["contact_name"];

            Assert.AreEqual("(Unknown)", result.Value);
        }
    }
}
