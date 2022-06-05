using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Server.IIS.Core;
using YouGotMailAPI.Models;
using System.IO;
using RestSharp;
using RestSharp.Authenticators;
using YouGotMailAPI.Database;
using YouGotMailAPI.EmailServices;


namespace YouGotMailAPI.Managers
{
    public class MailManager
    {
        
        private EMailManager _eMailManager = new EMailManager();
        private static int detectedNo = 0;
        private static int count = 0;

        private static readonly List<Mail> mails = new List<Mail>()
        {
            new Mail(_nextId++, 1651649329, "detected", ""),
            new Mail(_nextId++, 1651649320, "detected", ""),
            new Mail(_nextId++, 1651649384, "detected", ""),
        };

        private List<string> messages = new List<string>(){
            "Tømt","Der er en smule post", "Der er mere post", "Der er meget post", "Postkassen skal tømmes nu", "Jeg er helt fyldt" 
        };
        private static int _nextId = 1;

        public IEnumerable<Mail> GetAllMail() {
            IEnumerable<Mail> restult = new List<Mail>(mails);
            var sortedList = mails.OrderByDescending(d => d.UnixTimeStamp).ToList();
            return sortedList;
        }

        public Mail GetMailById(int id)
        {
            return mails.Find(i => i.Id == id);
        }

        public Mail AddMail(Mail newMail)
        {
            //newMail.Id = _nextId++;
            newMail.UnixTimeStamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            if (newMail.Detected == "yes"){
                if (count < 6){
                    count++;
                }

                newMail.Message = messages[count];
                mails.Add(newMail);
                EmailNotification();
                detectedNo = 0;
            }
            else if (newMail.Detected == "no" && detectedNo == 0)
            {
                mails.Add(newMail);
                detectedNo = 1;
                count = 0;
            }

            return newMail;
        }

        public void EmailNotification()
        {
            foreach (var userEmailOptions in _eMailManager.GetAllUserEmailOptions())
            {
                {
                    userEmailOptions.Body = "DU HAR FÅET POST I DIN ÆGTE POSTKASSE! :)";
                    userEmailOptions.Subject = "You Got Mail - Tjek Din Postkasse, heyo";
                    //ToEmails = "minecraftpecow@gmail.com"
                };

                var result = EmailService.SendEmail(userEmailOptions);
            }
        }



        public Mail DeleteMail(int idToBeDeleted)
        {
            Mail mailTobeDeleted = GetMailById(idToBeDeleted);
            if (mailTobeDeleted == null) throw new ArgumentNullException("idToBeDeleted", "Mail not found"); // kommer aldrig her ind??
            mails.Remove(mailTobeDeleted);
            return mailTobeDeleted;
        }
    }
}
