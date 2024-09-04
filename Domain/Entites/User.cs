using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class User : BaseEntity
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public int? StoreID { get; set; }
        public int? RoleID { get; set; }
        public string? Gender { get; set; }
        public Int64? Wallet { get; set; }
        public string? ConfirmationToken { get; set; }
        public bool IsConfirmed { get; set; }
        public int OrderID { get; set; }
        // R

        public virtual Role Role {  get; set; }   
        public virtual ICollection<ProductOrder> ProductOrders { get; set; }
        public virtual Wallets Wallets { get; set; }
        public virtual ICollection<ServiceOrder> ServiceOrders { get; set; }
        public virtual Store Store { get; set; }





    }
}
