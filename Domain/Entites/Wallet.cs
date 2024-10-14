using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class Wallet: BaseEntity
    {
        public Int64? Cash {  get; set; }
        //R
        public virtual User? User { get; set; }  
        public virtual ICollection<Transaction>? Transactions { get; set; }
    }
}
