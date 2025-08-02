using ZAMETKI_FINAL.Model;

namespace ZAMETKI_FINAL.Abstraction
{
    public interface IJwtTokenGenerator
    {
        JwtToken Generate(User user);
    }
}
