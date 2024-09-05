using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class ProductOfStore
    {
       public int StoreID { get; set; }
       public int ProductID { get; set; }
       public string? Note { get; set; }
       public Int64 Quantity {  get; set; }
        


    }
}
