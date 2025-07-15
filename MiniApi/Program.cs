using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using MiniApi.Abstraction;
using MiniApi.Model;
using MiniApi.Services;
using static MiniApi.Model.Note;

namespace MiniApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
        var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<IUserInterface, UserService>();
            builder.Services.AddSingleton<INoteInterface, NoteService>();

 
            var notes = new List<Note>();


            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            var users = new List<User>
            {
                new()
                {
                    UserId = 1,
                    Login = "Pidor1"
                },

                new()
                {
                    UserId = 2,
                    Login = "Pidor2"
                }

            };

            app.RegisterUserEndpoints();
            app.RegisterNoteEndpoints();

            app.Run();
        }
    }
    public static class EndpointsExtensions
    {
        private static readonly UserService _userService;
        private static readonly NoteService _noteService;
        public static void RegisterUserEndpoints(this WebApplication app)
        {
            app.MapGet("/users", (IUserInterface userService) => _userService.GetAllUser())
                .WithName("GetAllUsers");

            app.MapPost("/users", ([FromBody] User user, IUserInterface userService) => _userService.Registration(user.Login))
                .WithName("CreateUser");
        }

        public static void RegisterNoteEndpoints(this WebApplication app)
        {
            app.MapGet("/users/{userId}/notes", (int UserId, INoteInterface noteService) => _noteService.GetAllNoteUser(UserId))
                .WithName("GetAllUserNotes");

            app.MapPost("/users/{userId}/notes", (int userId, [FromBody] Note note, INoteInterface noteService) => _noteService.CreateNoteForUser(note.Description,note.Title,note.NotePriority,note.User))
                .WithName("CreateNote");
            
            app.MapPut("/users/{userId}/notes/{noteId}", (int userId, int noteId, [FromBody] Note note, INoteInterface noteService) => _noteService.UpdateNote(noteId, note.Title, note.Description))
                .WithName("UpdateNote");
            
            app.MapDelete("/users/{userId}/notes/{noteId}", (int userId, int noteId, INoteInterface noteService) => _noteService.DeleteNote(noteId))
                .WithName("DeleteNote");
            
        }
    }
}
