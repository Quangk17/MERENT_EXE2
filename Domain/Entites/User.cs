using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class User : BaseEntity
    {
        public string? name { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public string? phoneNumber { get; set; }
        public int? storeID { get; set; }
        public int? roleID { get; set; }
        public string? gender { get; set; }
        public Int64? Wallet { get; set; }
        public string? ConfirmationToken { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
