using Microsoft.VisualStudio.TestTools.UnitTesting;
using YouGotMailAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouGotMailAPI.Managers;

namespace YouGotMailAPI.Models.Tests
{
    [TestClass()]
    public class MailTests
    {
        private static int _nextId = 1;
        private Mail _mail;


        [TestInitialize]
        public void SetUp()
        {
            _mail = new Mail(_nextId, 1638383838, "detected", "");
        }
        [TestMethod()]
        public void TestConstructor()
        {
            Assert.AreEqual(1, _mail.Id);
            Assert.AreEqual(1638383838, _mail.UnixTimeStamp);
            Assert.AreEqual("detected", _mail.Detected);
            //Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            //    new Mail(4, 56, "detected"));
            //Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            //    new Mail(4, 1638383838, "notDetected"));
        }

        [TestMethod()]
        public void TestTimeStamp()
        {
            Assert.AreEqual(1638383838, _mail.UnixTimeStamp);
            //Assert.ThrowsException<ArgumentOutOfRangeException>(() => _mail.UnixTimeStamp = 957262944);
            //Assert.ThrowsException<ArgumentOutOfRangeException>(() => _mail.UnixTimeStamp = 1599999999);
            Assert.IsTrue(_mail.UnixTimeStamp > 1600000000);
        }

        [TestMethod()]
        public void TestDetected()
        {
            Assert.AreEqual("detected", _mail.Detected);
            Assert.ThrowsException<ArgumentNullException>(() => _mail.Detected = null);
        }
    }
}