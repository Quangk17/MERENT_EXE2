using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class Product: BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ProductType { get; set; }
        public Int64? Price { get; set; }
        
        //R

        public virtual ICollection<ProductOfStore> Store { get; set; }
        public virtual ICollection<ComboOfProduct> Combo { get; set; }  
        public virtual ICollection<ProductOrderDetails> OrderDetails { get; set; }  

    }
}
