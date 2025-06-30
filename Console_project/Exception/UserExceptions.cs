using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_project.Exceptions
{
    [Serializable]
    internal class UserNotLoggedException : System.Exception
    {
        public UserNotLoggedException() : base("Not logged") { }
    }
}
