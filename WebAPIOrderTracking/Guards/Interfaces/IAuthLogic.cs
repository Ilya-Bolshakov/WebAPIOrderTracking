using WebAPIOrderTracking.Models.Authefication;
using WebAPIOrderTracking.Models.Entities;

namespace WebAPIOrderTracking.Guards.Interfaces
{
    public interface IAuthLogic
    {
        bool TryLogin(User user, LoginModel loginModel, out string result);
    }
}
