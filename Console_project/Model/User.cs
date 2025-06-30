using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_project.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; } 
        public List<Note> Notes { get; set; } = new List<Note>();
    }
}
