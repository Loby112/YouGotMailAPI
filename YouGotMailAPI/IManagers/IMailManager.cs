using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouGotMailAPI.Models;

namespace YouGotMailAPI.Managers
{
    public interface IMailManager
    {
        public abstract IEnumerable<Mail> GetAllMail();
        public abstract Mail GetMailById(int id);
        public abstract Mail AddMail(Mail newMail);
        public abstract void EmailNotification();
        public abstract Mail DeleteMail(int idToBeDeleted);
    }
}
