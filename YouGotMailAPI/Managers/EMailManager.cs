using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using YouGotMailAPI.Models;
using YouGotMailAPI.EmailServices;

namespace YouGotMailAPI.Managers
{
    public class EMailManager
    {
        private static int _nextId = 1;

        private static List<UserEmailOptions> eMails = new List<UserEmailOptions>();

        public IEnumerable<UserEmailOptions> GetAllUserEmailOptions()
        {
            IEnumerable<UserEmailOptions> result = new List<UserEmailOptions>(eMails);
            return result;
        }

        public UserEmailOptions GetEmailOptionsById(int id)
        {
            return eMails.Find(e => e.Id == id);
        }

        public UserEmailOptions AddUserEmailOptions(UserEmailOptions newEmailOptions)
        {
            newEmailOptions.Id = _nextId;
            eMails.Add(newEmailOptions);
            return newEmailOptions;
        }

        public UserEmailOptions DeleteUserEmailOptions(int idToBeDeleted)
        {
            UserEmailOptions userEmailOptionsToBeDeleted = GetEmailOptionsById(idToBeDeleted);
            if (userEmailOptionsToBeDeleted == null) return null;
            eMails.Remove(userEmailOptionsToBeDeleted);
            return userEmailOptionsToBeDeleted;
        }

    }
}
