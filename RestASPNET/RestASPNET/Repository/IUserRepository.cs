using RestASPNET.Data.VO;
using RestASPNET.Model;

namespace RestASPNET.Repository
{
    public interface IUserRepository
    {
        User ValidateCredentials(UserVO user);
        User ValidateCredentials(string userName);

        bool RevokeToken(string userName);
        User RefreshUserInfo(User user);
    }
}
