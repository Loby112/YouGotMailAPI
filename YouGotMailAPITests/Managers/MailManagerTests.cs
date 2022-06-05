using Microsoft.VisualStudio.TestTools.UnitTesting;
using YouGotMailAPI.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouGotMailAPI.Models;
using RestSharp;
using RestSharp.Authenticators;

namespace YouGotMailAPI.Managers.Tests
{
    [TestClass()]
    public class MailManagerTests
    {
        private MailManager _mailManager;
        private static int _nextId = 1;
        private List<Mail> mails;

        [TestInitialize]
        public void Setup()
        {
            _mailManager = new MailManager();
            mails = _mailManager.GetAllMail().ToList();
            //Mail testMail = new Mail(1, 23974598, "detected");
            //mails.Add(testMail);
            
            // Lav dummy test data her, sådan at dataen ikke er andre steder
        }

        [TestMethod()]
        public void GetAllMailTest()
        {
            int beforeAddCount = _mailManager.GetAllMail().ToList().Count;
            Assert.AreEqual(beforeAddCount, mails.Count);
            Assert.AreNotEqual(beforeAddCount + 1, mails.Count);
            Assert.AreNotEqual(beforeAddCount - 1, mails.Count);
        }

        [TestMethod()]
        public void AddAndDeleteMailTest()
        {
            //testing add with detected = "yes"
            int beforeAddCount = _mailManager.GetAllMail().ToList().Count;
            int defaultId = 0;
            Mail newMail = new Mail(_nextId, 1651649329, "yes", "");

            Mail addedMail = _mailManager.AddMail(newMail);
            int newId = newMail.Id;

            Assert.AreNotEqual(defaultId, newId);
            Assert.AreEqual(beforeAddCount + 1, _mailManager.GetAllMail().ToList().Count);

            //testing add with detected = "no"
            Mail newMail2 = new Mail(_nextId, 1651649329, "no", "");
            Mail addedMail2 = _mailManager.AddMail(newMail2);
            int newId2 = newMail2.Id;

            Assert.AreNotEqual(defaultId, newId2);
            Assert.AreEqual(beforeAddCount + 2, _mailManager.GetAllMail().ToList().Count);

            //delete
            Mail mailToBeDeleted = _mailManager.DeleteMail(newId);
            Mail mailToBeDeleted2 = _mailManager.DeleteMail(newId2);
            Assert.AreEqual(beforeAddCount, _mailManager.GetAllMail().Count());
            //Assert.IsNull(_mailManager.DeleteMail(237492));
            Assert.ThrowsException<ArgumentNullException>(() => _mailManager.DeleteMail(2345678));
        }

        //[TestMethod()]
        //public void DeleteMailTest()
        //{
        //    int beforeAddCount = _mailManager.GetAllMail().ToList().Count;
        //    _mailManager.DeleteMail()
        //    Assert.AreEqual(beforeAddCount, _mailManager.GetAllMail().Count());
        //}
    }
}