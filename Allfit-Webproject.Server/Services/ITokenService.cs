namespace Allfit_Webproject.Server.Services
{
    public interface ITokenService
    {
        string CreateToken(string userId, string email);
    }
}
