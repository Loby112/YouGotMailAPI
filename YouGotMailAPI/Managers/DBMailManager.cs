using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouGotMailAPI.Database;
using YouGotMailAPI.EmailServices;
using YouGotMailAPI.Models;

namespace YouGotMailAPI.Managers
{
    public class DBMailManager : IMailManager{
        private DBEmailManager _eMailManager;
        private static int detectedNo = 0;
        private MailDBContext _context = new MailDBContext();
        private static int count = 0;
        private List<string> messages = new List<string>(){
            "Tømt","Der er en smule post", "Der er mere post", "Der er meget post", "Postkassen skal tømmes nu", "Jeg er helt fyldt"
        };

        public DBMailManager(MailDBContext context)
        {
            
            _eMailManager = new DBEmailManager(context);
        }

        public  IEnumerable<Mail> GetAllMail()
        {
            IEnumerable<Mail> result = _context.Mail; 
            var sortedList = result.OrderByDescending(d => d.UnixTimeStamp).ToList();
            return sortedList;
        }

        public  Mail GetMailById(int id)
        {
            return _context.Mail.Find(id);
        }

        public  Mail AddMail(Mail newMail)
        {
            newMail.Id = 0;
            newMail.UnixTimeStamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            if (newMail.Detected == "yes")
            {
                if (count < 6) {
                    count++;
                }

                newMail.Message = messages[count];
                _context.Mail.Add(newMail);
                EmailNotification();
                detectedNo = 0;
            }
            else if (newMail.Detected == "no" && detectedNo == 0)
            {
                count = 0;
                newMail.Message = messages[count];
                _context.Mail.Add(newMail);
                detectedNo = 1;
                
            }

            _context.SaveChanges();
            return newMail;
        }

        public  void EmailNotification()
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

        public  Mail DeleteMail(int idToBeDeleted)
        {
            Mail mailTobeDeleted = GetMailById(idToBeDeleted);
            if (mailTobeDeleted == null) throw new ArgumentNullException("idToBeDeleted", "Mail not found"); // kommer aldrig her ind??
            _context.Mail.Remove(mailTobeDeleted);
            _context.SaveChanges();
            return mailTobeDeleted;
        }
    }
}
