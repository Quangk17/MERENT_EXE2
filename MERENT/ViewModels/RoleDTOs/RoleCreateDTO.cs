using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.RoleDTOs
{
    public class RoleCreateDTO
    {
        public bool IsDeleted { get; set; } = false;

        public string? RoleName { get; set; } = null;
    }
}
