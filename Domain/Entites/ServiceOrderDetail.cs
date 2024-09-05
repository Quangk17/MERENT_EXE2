using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class ServiceOrderDetail: BaseEntity
    {
        public int ServiceOrderID {  get; set; }
        public int ServiceID { get; set; }
        public Int64 Quantity { get; set; }
        public Int64 UnitPrice { get; set; }

        //R

        public virtual ServiceOrder? ServiceOrder { get; set; }
        public virtual Service? Service { get; set; }

    }
}
