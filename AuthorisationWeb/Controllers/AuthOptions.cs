using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace AuthorizationWeb.Controllers
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; // издатель токена
        public const string AUDIENCE = "http://localhost:5000/api/userservice/"; // потребитель токена
        const string KEY = "this is my custom Secret key for authnetication";   // ключ для шифрации
        public const int LIFETIME = 1; // время жизни токена - 1 минутa
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}