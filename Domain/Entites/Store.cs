using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class Store: BaseEntity
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        //R

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<ServiceOfStore> ServiceOfStores { get; set; }
        public virtual ICollection<ProductOfStore> ProductOfStores { get; set; }

    }
}
