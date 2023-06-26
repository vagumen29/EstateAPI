namespace Application.Common.Interfaces
{
    public interface ITokenService
    {
        string CreateJwtToken(string id);
    }
}
