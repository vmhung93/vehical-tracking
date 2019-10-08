using System.Threading.Tasks;
using VehicalTracking.Service.Models;

namespace VehicalTracking.Service.User
{
    public interface IUserService
    {
        Task<string> SignIn(SignInModel signInModel);

        Task<string> SignUp(SignUpModel signUpModel);
    }
}
