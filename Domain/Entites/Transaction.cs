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
        public Int64 TotalAmount { get; set; }
        public string? PaymentType { get; set; }

        //relation
        public virtual Wallet? Wallets { get; set; }
    }
}
