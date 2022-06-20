using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Event.Web.Authorization
{
    public static class IdentityExtension
    {
        public static int UserId(this IEnumerable<Claim> claims)
        {
            var claim = claims.SingleOrDefault(t => t.Type == ClaimNameConstants.UserId);
            if (claim != null)
            {
                return int.Parse(claim.Value);
            }
            throw new KeyNotFoundException($"Claim type: '{ClaimNameConstants.UserId}' can't be found");
        }
    }
}
