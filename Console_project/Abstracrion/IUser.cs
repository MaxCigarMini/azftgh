using Console_project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_project.Interface
{
    public interface IUser
    {
        User Registration(string username);
        User Login(string Name);
    }
}
