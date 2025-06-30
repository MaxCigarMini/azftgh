using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_project.Exceptions
{
    [Serializable]
    internal class NoteNotFoundException : System.Exception
    {
        public NoteNotFoundException(int id) : base($"Note with id = {id} is not found.") { }
    }
}
