using Microsoft.VisualStudio.TestTools.UnitTesting;
using YouGotMailAPI.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouGotMailAPI.Database;
using YouGotMailAPI.EmailServices;

namespace YouGotMailAPI.Managers.Tests {
    [TestClass()]
    public class DBEmailManagerTests {
        private DBEmailManager _EmailManager;
        private List<UserEmailOptions> userEmailOptions;
        MailDBContext context = new MailDBContext();

        [TestInitialize]
        public void Setup() {
            _EmailManager = new DBEmailManager(context);
            userEmailOptions = _EmailManager.GetAllUserEmailOptions().ToList();
        }

        [TestMethod()]
        public void GetAllUserEmailOptionsTest() {
            int beforeAddCount = _EmailManager.GetAllUserEmailOptions().Count();
            Assert.AreEqual(beforeAddCount, userEmailOptions.Count);
            Assert.AreNotEqual(beforeAddCount + 1, userEmailOptions.Count);
            Assert.AreNotEqual(beforeAddCount - 1, userEmailOptions.Count);
        }




        [TestMethod()]
        public void AddAndDeleteUserEmailOptionsTest() {
            int beforeAddCount = _EmailManager.GetAllUserEmailOptions().ToList().Count;
            int defaultId = 0;
            UserEmailOptions newEmail = new UserEmailOptions(0, "Minecraftpecow@gmail.com", "", "");

            UserEmailOptions addedEmail = _EmailManager.AddUserEmailOptions(newEmail);
            int newId = newEmail.Id;

            Assert.AreNotEqual(defaultId, newId);
            Assert.AreEqual(beforeAddCount + 1, _EmailManager.GetAllUserEmailOptions().ToList().Count);
            Assert.AreEqual("Minecraftpecow@gmail.com", addedEmail.ToEmails);

            UserEmailOptions EmailToBeDeleted = _EmailManager.DeleteUserEmailOptions(newId);
            Assert.AreEqual(beforeAddCount, _EmailManager.GetAllUserEmailOptions().Count());


        }
    }
}