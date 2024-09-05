using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class ServiceOfStore
    {
        public int StoreID { get; set; }
        public int ServiceID { get; set; }
        public string? Note { get; set; }

        //R

        public virtual Store? Store { get; set; }
        public virtual Service? Service { get; set; }
    }
}
