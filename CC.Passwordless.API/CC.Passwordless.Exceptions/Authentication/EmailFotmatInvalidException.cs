using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.Passwordless.Exceptions.Authentication
{
    public class EmailFotmatInvalidException: Exception
    {
        public EmailFotmatInvalidException(string message) : base(message) { }
    }
}
