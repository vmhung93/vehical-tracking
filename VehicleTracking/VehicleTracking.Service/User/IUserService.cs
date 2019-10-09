using System.Threading.Tasks;
using VehicleTracking.Service.Models;

namespace VehicleTracking.Service.User
{
    public interface IUserService
    {
        Task<string> SignIn(SignInModel signInModel);

        Task<string> SignUp(SignUpModel signUpModel);
    }
}
