using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.ServiceOrderDetailDTOs
{
    public class ServiceOrderDetailDTOs
    {
        public int ServiceOrderID { get; set; }
        public int ServiceID { get; set; }
        public Int64 Quantity { get; set; }
        public Int64 UnitPrice { get; set; }
    }
}
