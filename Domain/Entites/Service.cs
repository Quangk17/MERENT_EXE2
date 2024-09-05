using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class Service: BaseEntity
    {
        public string? Name {  get; set; }
        public string? Description { get; set; }
        public Int64? Price { get; set; }

        //R

        public virtual ICollection<ServiceOrderDetail>? OrderDetails { get; set; }   
        public virtual ICollection<ServiceOfStore>? StoreDetails { get; set; }   

    }
}
