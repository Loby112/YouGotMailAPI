using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouGotMailAPI.EmailServices;

namespace YouGotMailAPI.Managers
{
    interface IEmailManager
    {
        IEnumerable<UserEmailOptions> GetAllUserEmailOptions();
        UserEmailOptions GetEmailOptionsById(int id);
        UserEmailOptions AddUserEmailOptions(UserEmailOptions newEmailOptions);
        UserEmailOptions DeleteUserEmailOptions(int idToBeDeleted);
    }
}
