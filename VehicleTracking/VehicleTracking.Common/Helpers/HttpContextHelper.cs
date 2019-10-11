using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace VehicleTracking.Common.Helpers
{
    public static class HttpContextHelper
    {
        private static IHttpContextAccessor _httpContextAccessor;

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Get current user id 
        /// </summary>
        /// <returns></returns>
        public static Guid GetCurrentUserId()
        {
            var claims = _httpContextAccessor.HttpContext.User;

            if (!claims.Identity.IsAuthenticated)
            {
                throw new Exception("HttpContextHelper - GetCurrentUserId - User has not been authenticated");
            }

            var identifierClaim = claims.FindFirst(ClaimTypes.NameIdentifier);

            if (identifierClaim == null)
            {
                throw new Exception("HttpContextHelper - GetCurrentUserId - Cannot get user id from claim");
            }

            return new Guid(identifierClaim.Value);
        }
    }
}
