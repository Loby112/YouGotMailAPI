using Microsoft.VisualStudio.TestTools.UnitTesting;
using YouGotMailAPI.EmailServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace YouGotMailAPI.EmailServices.Tests
{
    [TestClass()]
    public class UserEmailOptionsTests
    {
        private static int _nextId = 1;
        private UserEmailOptions _userEmailOptions;

        [TestInitialize]
        public void SetUp()
        {
            _userEmailOptions = new UserEmailOptions(0,"TestMail@gmail.com","","");
            
        }


        [TestMethod()]
        public void UserEmailToEmailsTest()
        {
            //UserEmailOptions falskeEmail = new UserEmailOptions("mailudensnabela", "", "");

            Assert.ThrowsException<ArgumentException>(() => _userEmailOptions.ToEmails = "mailudensnabela");
            Assert.ThrowsException<ArgumentNullException>(() => _userEmailOptions.ToEmails = null);
        }

        
    }
}