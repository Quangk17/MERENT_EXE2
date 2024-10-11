using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utils
{
    public static class AuthenTools
    {
        public static string GetCurrentUserId(ClaimsIdentity identity)
        {
            if (identity == null)
            {
                return null;
            }

            // Tìm claim với loại "Name" (hoặc có thể là một loại khác nếu bạn lưu Id trong claim khác)
            return identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        }

    }
}
