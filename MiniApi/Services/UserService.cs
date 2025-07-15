using MiniApi.Abstraction;
using MiniApi.Model;

namespace MiniApi.Services
{
    public class UserService : IUserInterface
    {
        private static User user;
        private readonly List<User> _users = new List<User>();

        public User Registration(string login)
        {
            if (string.IsNullOrEmpty(login))
                throw new ArgumentException(nameof(login));

            var NewUser = new User
            {
                UserId = _users.Count > 0 ? _users.Max(n => n.UserId) + 1 : 1,
                Login = login
            };
            _users.Add(NewUser);
            return NewUser;
        }
        public IEnumerable<User> GetAllUser()
        {
            return _users.ToList();
        }
    }
}
