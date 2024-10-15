using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class ComboOfProduct
    {
        public int ProductID { get; set; }
        public int ComboID { get; set; }
        public string? Description { get; set; }
        public Int64 Quantity { get; set; }

        //R
        public virtual Product? Product { get; set; }
        public virtual Combo? Combo { get; set; }

    }
}
