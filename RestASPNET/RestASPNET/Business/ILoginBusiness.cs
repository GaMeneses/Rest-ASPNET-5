using RestASPNET.Data.VO;

namespace RestASPNET.Business
{
    public interface ILoginBusiness
    {
        TokenVO ValidateCredentials(UserVO user);
        TokenVO ValidateCredentials(TokenVO token);

        bool RevokeToken(string userName);
    }
}
