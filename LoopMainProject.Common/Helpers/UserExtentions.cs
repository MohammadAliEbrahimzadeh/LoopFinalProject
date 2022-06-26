using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LoopMainProject.Common.Helpers
{
    public static class UserExtentions
    {
        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal == null)
            {
                throw new ArgumentNullException(nameof(claimsPrincipal));
            }

            return claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
