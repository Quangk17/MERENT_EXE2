using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class Service: BaseEntity
    {
        public string? name {  get; set; }
        public string? description { get; set; }
        public Int64? price { get; set; }
    }
}
