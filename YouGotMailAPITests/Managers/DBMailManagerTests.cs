using Microsoft.VisualStudio.TestTools.UnitTesting;
using YouGotMailAPI.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YouGotMailAPI.Database;
using YouGotMailAPI.Models;

namespace YouGotMailAPI.Managers.Tests {
    [TestClass()]
    public class DBMailManagerTests{

        MailDBContext context = new MailDBContext();
        private DBMailManager manager;
        private List<Mail> mails = new List<Mail>();


        [TestInitialize]
        public void Init(){

            manager = new DBMailManager(context);
        }

        [TestMethod]
        public void AddDeleteGetAll(){
            Mail testMail = new Mail(0, 0, "yes", "");
            
            int beforeAddCount = manager.GetAllMail().ToList().Count;
            manager.AddMail(testMail);
            //manager.AddMail(testMail2);

            // Her tester vi at ID bliver sat automatisk
            Assert.AreNotEqual(0, manager.GetAllMail().ToList()[0].Id);
            // Her vises det at den rent faktisk er oprettet
            Assert.AreEqual(beforeAddCount + 1, manager.GetAllMail().ToList().Count);
            //Assert.AreEqual("Tømt", manager.GetAllMail().ToList()[0].Message);
            
            mails = manager.GetAllMail().ToList();
            // Her tæller listen før vi sletter noget 
            beforeAddCount = manager.GetAllMail().ToList().Count;

            // Her tester vi om vi får den besked vi forventer med den første mail
            Assert.AreEqual("Der er en smule post", mails[0].Message);
            manager.DeleteMail(mails[0].Id);
            // Her tester vi om vi rent faktisk har slettet noget
            Assert.AreEqual(beforeAddCount - 1, manager.GetAllMail().ToList().Count);

            Mail testMail2 = new Mail(0, 0, "no", "");
            manager.AddMail(testMail2);
            Assert.AreEqual("Tømt", manager.GetAllMail().ToList()[0].Message);




        }

    }
}