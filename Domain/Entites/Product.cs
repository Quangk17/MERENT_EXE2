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
        public string? productType { get; set; }
        public Int64? price { get; set; }

    }
}
