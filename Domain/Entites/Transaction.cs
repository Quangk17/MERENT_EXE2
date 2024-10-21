using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class Transaction: BaseEntity
    {
       public int WalletId { get; set; }
        public long TotalAmount { get; set; }
        public string? PaymentType { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }

        //relation
        public virtual Wallet? Wallets { get; set; }
    }
}
