using MiniApi.Model;

namespace MiniApi.Abstraction
{
    public interface IUserInterface
    {
        public User Registration(string login);
        IEnumerable<User> GetAllUser();

    }
}
