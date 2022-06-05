using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouGotMailAPI.Database;
using YouGotMailAPI.EmailServices;

namespace YouGotMailAPI.Managers
{
    public class DBEmailManager : IEmailManager
    {
        private MailDBContext _context;
        public DBEmailManager(MailDBContext context)
        {
            _context = context;
        }

        private static List<UserEmailOptions> eMails = new List<UserEmailOptions>();
        public IEnumerable<UserEmailOptions> GetAllUserEmailOptions(){
            IEnumerable<UserEmailOptions> result = _context.Emails;
            return result;
        }

        public UserEmailOptions GetEmailOptionsById(int id)
        {
            return _context.Emails.Find(id);
        }

        public UserEmailOptions AddUserEmailOptions(UserEmailOptions newEmailOptions)
        {
            newEmailOptions.Id = 0;
            _context.Emails.Add(newEmailOptions);
            _context.SaveChanges();
            return newEmailOptions;
        }

        public UserEmailOptions DeleteUserEmailOptions(int idToBeDeleted)
        {
            UserEmailOptions userEmailOptionsToBeDeleted = GetEmailOptionsById(idToBeDeleted);
            if (userEmailOptionsToBeDeleted == null) return null;
            _context.Emails.Remove(userEmailOptionsToBeDeleted);
            _context.SaveChanges();
            return userEmailOptionsToBeDeleted;
        }
    }
}
