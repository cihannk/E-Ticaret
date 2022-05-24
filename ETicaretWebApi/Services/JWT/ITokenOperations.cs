using ETicaretWebApi.Entitites;

namespace ETicaretWebApi.Services.JWT
{
    public interface ITokenOperations
    {
        string GetToken(User user);
    }
}
