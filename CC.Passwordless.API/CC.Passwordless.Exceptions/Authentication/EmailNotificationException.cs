using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.Passwordless.Exceptions.Authentication
{
    public class EmailNotificationException : Exception
    {
        public EmailNotificationException(string message) : base(message) { }
    }
}
