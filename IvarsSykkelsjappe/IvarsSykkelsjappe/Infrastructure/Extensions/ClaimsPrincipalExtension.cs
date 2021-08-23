using System;
using System.Security.Claims;

namespace IvarsSykkelsjappe.Infrastructure.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static string GetId(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}
