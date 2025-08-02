using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Xml.Linq;
using ZAMETKI_FINAL.Abstraction;
using ZAMETKI_FINAL.Model;

namespace ZAMETKI_FINAL.Services
{
    public class UserService : IUserInterface
    {
        private static readonly List<User> users = new List<User>();
        private static User? _currentUser;
        private const string UserFileJSON = "user.json";

        public User CreateUser(string userName, string password)
        {
            ValidateUserName(userName);
            ValidateUserPassword(password);

            var newUser = new User
            {
                UserId = users.Count > 0 ? users.Max(n => n.UserId) + 1 : 1,
                UserName = userName,
                Password = password
            };

            users.Add(newUser);

            SaveUser();

            return newUser;
        }

        public User Login(string userName)
        {
            ValidateUserName(userName);

            _currentUser = users.FirstOrDefault(u => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));

            return _currentUser;
        }

        public void Logout()
        {
            if (_currentUser == null)
                throw new KeyNotFoundException("Вы уже разлогинены");
   
            _currentUser = null;
        }

        public void SaveUser()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(users, options);
                File.WriteAllText(UserFileJSON, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении пользователей: {ex.Message}");
            }
        }







        private static void ValidateUserPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Пароль не может быть пустым", nameof(password));
        }

        private static void ValidateUserName(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Имя пользователя не может быть пустым", nameof(username));
        }
    }
}
