using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.ProductOrderDTOs
{
    public class ProductOrderUpdateDTO
    {
        public string? Decription { get; set; }
        public DateTime? OrderDate { get; set; }
        public Int64? TotalAmount { get; set; }
        public Int64? TotalPrice { get; set; }
        public int? UserID { get; set; }
    }
}
