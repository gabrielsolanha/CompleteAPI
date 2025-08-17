namespace Completeapi.CsharpModel.Common.Security
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(IUser user);
        UserInfo GetUserInfoFromToken(string token);
    }
}
