using Console_project.Interface;
using Console_project.Model;
using Microsoft.AspNetCore.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Console_project.Repository
{
    public class UserService : IUser
    {
        private const string UserFileJSON = "user.json";
        private User  _currentUser;
        private int _lastUserId;

        private List<User> _users = new List<User>();

        public User Registration(string username)
        {
            ValidateUsername(username);

            if (_users.Any(u => u.Name.Equals(username, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException($"User '{username}' already exists");

            if (_currentUser != null)
                throw new InvalidOperationException("User already exists");

            var newUser = new User
            {
                UserId = ++_lastUserId,
                Name = username,
                Notes = new List<Note>()
            };

            _users.Add(newUser);
            SaveUser();
            return newUser;
        }
        public User GetUserByName(string name)
        {
            ValidateUsername(name);

            return _users.FirstOrDefault(u =>
                u.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public User Login(string username)
        {
            ValidateUsername(username);
            _currentUser = GetUserByName(username);
            return _currentUser;
        }
        public bool UserExists(string username)
        {
            ValidateUsername(username);
            return _users.Any(u =>
                u.Name.Equals(username, StringComparison.OrdinalIgnoreCase));
        }
        private static void ValidateUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username cannot be empty or whitespace", nameof(username));
        }
        public void SaveUser()
        {
            try
            {
                var options = new JsonSerializerOptions{WriteIndented = true};
                string json = JsonSerializer.Serialize(_users, options);
                File.WriteAllText(UserFileJSON, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении пользователей: {ex.Message}");
            }
        }
        public void ClearUser()
        {
            try
            {
                if (File.Exists(UserFileJSON))
                    File.Delete(UserFileJSON);
            }
            catch (Exception)
            {
                Console.WriteLine("файла не существует");
            }
            _currentUser = null;
        }
        public void LoadUser()
        {
            try
            {
                if (File.Exists(UserFileJSON))
                {
                    string json = File.ReadAllText(UserFileJSON);
                    _users = JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();

                    _lastUserId = _users.Count > 0 ? _users.Max(u => u.UserId) : 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке пользователей: {ex.Message}");
                _users = new List<User>();
                _lastUserId = 0;
            }
        }
    }
}
