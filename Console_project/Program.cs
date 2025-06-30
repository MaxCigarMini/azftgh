using Console_project.Model;
using Console_project.Repository;
using System.Threading.Channels;
using Console_project.Model;
using Console_project.Repository;
using Console_project.Service;

namespace Console_project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UserService userService = new UserService();
            NoteService noteService = new NoteService();
            ConsoleControle consoleControle = new ConsoleControle();


            ConsoleControle.RUN();
        }
    }
}
