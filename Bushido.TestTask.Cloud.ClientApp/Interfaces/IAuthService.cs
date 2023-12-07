using Bushido.TestTask.Cloud.Authentication.Models;
using Bushido.TestTask.Cloud.Authentication.Models.DBModel;

namespace Bushido.TestTask.Cloud.ClientApp.Interfaces
{
    public interface IAuthService
    {
        JWTtoken Token { get; set; }

        Task<JWTtoken> SignIn(SignInModel signInModel);
        Task<JWTtoken> SignUp(SignUpModel signUpModel);
    }
}