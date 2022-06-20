using System;
using System.Linq;
using Event.Common.Enum;
using Microsoft.AspNetCore.Authorization;

namespace Event.Web.Authorization
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params UserRole[] allowedRoles)
        {
            var allowedRolesAsStrings = allowedRoles.Select(x => Enum.GetName(typeof(UserRole), x));
            Roles = string.Join(",", allowedRolesAsStrings);
        }
    }
}
