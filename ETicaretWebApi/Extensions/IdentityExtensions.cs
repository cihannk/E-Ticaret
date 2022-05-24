using System.Security.Claims;
using System.Security.Principal;

namespace ETicaretWebApi.Extensions
{
    public static class IdentityExtensions
    {
        public static int GetId(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.Claims?.FirstOrDefault(claim => claim.Type == "Id");
            if (claim == null)
            {
                return 0;
            }
            return (int)Convert.ToInt64(claim.Value);
        }
    }
}
