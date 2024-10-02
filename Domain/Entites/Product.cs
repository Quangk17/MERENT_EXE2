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
        public string? URLCertain { get; set; }
        public string? URLLeft { get; set; }
        public string? URLRight { get; set; }

        
        //R

        public virtual ICollection<ProductOfStore>? ProductOfStores { get; set; }
        public virtual ICollection<ComboOfProduct>? ComboOfProducts { get; set; }  
        public virtual ICollection<ProductOrderDetails>? ProductOrderDetails { get; set; }  


    }
}
