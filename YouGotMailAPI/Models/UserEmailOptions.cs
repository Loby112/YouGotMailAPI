using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YouGotMailAPI.EmailServices
{
    public class UserEmailOptions
    {
        private string _toEmails;
        private int _id;

        public UserEmailOptions(int id, string toEmails, string subject, string body)
        {
            Id = id;
            ToEmails = toEmails;
            Subject = subject;
            Body = body;
            
        }

        public UserEmailOptions()
        {
            
        }
        [Key]
        public int Id {
            get => _id;
            set => _id = value;
        }
        public string ToEmails { get => _toEmails;
            set
            {
                if (value == null) throw new ArgumentNullException("ToEmails","Der skal indskrives mere end 0 bogstaver");
                if (!value.Contains("@")) throw new ArgumentException("Der skal være et '@' tegn.");
                _toEmails = value;
            } }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
