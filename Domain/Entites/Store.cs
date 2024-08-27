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
        public string? address { get; set; }
        public DateTime? startTime { get; set; }
        public DateTime? endTime { get; set; }

    }
}
