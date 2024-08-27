using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class ProductOrder : BaseEntity
    {
        public string? name { get; set; }
        public DateTime? orderDate { get; set; }
        public Int64? totalAmount { get; set; }
        public Int64? totalPrice { get; set; }
        public int? userID { get; set; }
        public int? orderDetailID { get; set; }

        }
    }
