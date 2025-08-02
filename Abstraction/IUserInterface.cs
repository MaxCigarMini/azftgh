using ZAMETKI_FINAL.Model;

namespace ZAMETKI_FINAL.Abstraction
{
    public interface IUserInterface
    {
        public User CreateUser(string userName, string password);
        public User Login(string userName);
        public void Logout();

        public void SaveUser();
    }
}
