using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.AccountDTOs
{
    public class UserDetailsModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public int? StoreID { get; set; }
        public string? Gender { get; set; }
        public int? WalletId { get; set; }
        public string? ConfirmationToken { get; set; }
        public bool IsConfirmed { get; set; }
        public int OrderID { get; set; }

        public RoleInfoModel? Role { get; set; } = null;
    }
}
