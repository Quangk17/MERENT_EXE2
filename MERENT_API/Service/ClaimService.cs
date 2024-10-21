using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Application.Interfaces;
using Application.Utils;

namespace MERENT_API.Service
{
    public class ClaimServices : IClaimsService
    {
        public ClaimServices(IHttpContextAccessor httpContextAccessor)
        {
            var identity = httpContextAccessor.HttpContext?.User?.Identity as ClaimsIdentity;
            var extractedId = AuthenTools.GetCurrentUserId(identity);

            GetCurrentUserId = string.IsNullOrEmpty(extractedId) ? 0 : Convert.ToInt32(extractedId);
        }

        public int GetCurrentUserId { get; }
    }
}
