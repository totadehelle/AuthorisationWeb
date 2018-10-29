using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace AuthorizationWeb.Controllers
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; // издатель токена
        public const string AUDIENCE = "http://localhost:5000/"; // потребитель токена
        const string KEY = "123";   // ключ для шифрации
        public const int LIFETIME = 20; // время жизни токена - 20 минут
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}