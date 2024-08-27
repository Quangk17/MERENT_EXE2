using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class ProductOrderDetails: BaseEntity
    {
        public int? productID {  get; set; }
        public Int64? quantity { get; set; }
        public Int64? unitPrice { get; set; }
    }
}
