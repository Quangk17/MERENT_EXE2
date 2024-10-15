using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.ServiceDTOs
{
    public class ServiceDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Int64? Price { get; set; }
        public string? UrlImg { get; set; }
    }
}
