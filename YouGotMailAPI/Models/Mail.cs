using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YouGotMailAPI.Models
{
    public class Mail
    {
        [Key]
        public int Id{
            get => _id;
            set => _id = value;
        }

        //private DateTime _timeStamp; // lav datetime om til Unix Time og ændre test herefter
        private Int32 _unixTimestamp;
        private string _detected;
        private int _id;
        private string _message;

        public Int32 UnixTimeStamp
        {
            get => _unixTimestamp;
            set
            {
                //value = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                //if (value < 1600000000) throw new ArgumentOutOfRangeException("UnixTimeStamp", "Must be more than 1600000000 sec.");
                _unixTimestamp = value;
            }
        }

        public string Detected
        {
            get => _detected;
            set
            {
                if (value == null) throw new ArgumentNullException("Detected", "Cannot be null");
                //if (value != "Detected") throw new ArgumentOutOfRangeException("Detected", "");
                _detected = value;
            }

        }

        public string Message{
            get => _message;
            set => _message = value;
        }
    
        public Mail()
        {

        }

        public Mail(int id, Int32 unixTimeStamp, string detected, string message)
        {
            Id = id;
            UnixTimeStamp = unixTimeStamp;
            Detected = detected;
            Message = message;
        }
    }
}
