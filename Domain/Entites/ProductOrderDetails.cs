using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class ProductOrderDetails: BaseEntity
    {
        public int? ProductID {  get; set; }
        public Int64? Quantity { get; set; }
        public Int64? UnitPrice { get; set; }

        //R
        public virtual ProductOrder ProductOrder { get; set; }
        public virtual Product Product { get; set; }

    }
}
