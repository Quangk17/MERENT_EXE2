using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class ServiceOrder: BaseEntity
    {
        public string? Name {  get; set; }
        public string? ServiceDate { get; set; }
        public Int64 TotalAmount { get; set; }
        public int? UserId { get; set; }
        public string? StatusOrder {  get; set; }
         
        //R
        public virtual User? User { get; set; }  
        public virtual ICollection<ServiceOrderDetail>?ServiceOrderDetails { get; set; }

    }
}
