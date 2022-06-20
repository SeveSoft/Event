using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Event.Common.Models;
using Event.Web.Services;
using Microsoft.AspNetCore.Authentication;

namespace Event.Web.Authorization
{
    public class AddClaimsTransformation : IClaimsTransformation
    {
        private readonly IEventDataService _eventDataService;

        public AddClaimsTransformation(IEventDataService eventDataService)
        {
            _eventDataService = eventDataService;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var nameIdentifier = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (nameIdentifier == null)
            {
                return principal;
            }

            var user = await _eventDataService.GetUserAsync(new UserServiceModel { IdentityId = nameIdentifier.Value });
            if (user == null)
            {
                return principal;
            }

            var clone = principal.Clone();
            var newIdentity = (ClaimsIdentity)clone.Identity;
            if (newIdentity != null)
            {
                newIdentity.AddClaim(new Claim(ClaimTypes.Role, user.UserRole.ToString()));
                newIdentity.AddClaim(new Claim(ClaimNameConstants.UserId, user.Id.ToString()));
            }
            return clone;
        }
    }
}
