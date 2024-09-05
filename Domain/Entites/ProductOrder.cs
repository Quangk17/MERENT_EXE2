using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class ProductOrder : BaseEntity
    {
        public string? Name { get; set; }
        public DateTime? OrderDate { get; set; }
        public Int64? TotalAmount { get; set; }
        public Int64? TotalPrice { get; set; }
        public int? UserID { get; set; }
        public int? OrderDetailID { get; set; }


        //R
        public virtual ICollection<ProductOrderDetails>? ProductOrderDetails { get; set; }
        public virtual User? User { get; set; }

        }
    }
