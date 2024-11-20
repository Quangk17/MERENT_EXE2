using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.ProductOrderDetailDTOs
{
    public class PODetailDTO
    {
        public int? ProductID { get; set; }
        public int? OrderId { get; set; }
        public Int64? Quantity { get; set; }
        public Int64? UnitPrice { get; set; }
    }
}
