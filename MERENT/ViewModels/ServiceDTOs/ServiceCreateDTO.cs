using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.ServiceDTOs
{
    public class ServiceCreateDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Int64? Price { get; set; }
    }
}
