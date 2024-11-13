using System.Security.Claims;
using Application.Interfaces;

namespace MERENT_API.Service
{
    public class ClaimServices : IClaimsService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimServices(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetCurrentUserId
        {
            get
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    return userId;
                }
                return -1; // Return -1 if no userId is found
            }
        }
    }
}
