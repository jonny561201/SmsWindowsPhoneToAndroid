using WindowsPhoneToAndroidSMSBackup.WindowsPhoneToAndroid;
using NUnit.Framework;

namespace WindowsPhoneToAndroidSMSBackup.WinowsPhoneToAndroid.Tests
{
    [TestFixture]
    public class MainTest
    {
        [Test]
        public void ExecuteMethod()
        {
            var execute = new Main();

            execute.Execute();
        }
    }
}
