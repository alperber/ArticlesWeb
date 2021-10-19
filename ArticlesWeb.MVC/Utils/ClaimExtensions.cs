using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace ArticlesWeb.MVC.Utils
{
    public static class ClaimExtensions
    {
        public static Guid GetUserId(this IEnumerable<Claim> claims)
        {
            var claim = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;

            return Guid.TryParse(claim, out var guid) ? guid : Guid.Empty;
        }
    }
}