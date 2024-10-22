using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class Wallet: BaseEntity
    {   //test
        public long? Cash {  get; set; }
        public int? UserId { get; set; }
        public string? WalletType { get; set; }
        //R
        public virtual User? User { get; set; }  
        public virtual ICollection<Transaction>? Transactions { get; set; }
    }
}
