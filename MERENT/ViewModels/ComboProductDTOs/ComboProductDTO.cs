using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.ComboProductDTOs
{
    public class ComboProductDTO
    {
        public int ProductID { get; set; }
        public int ComboID { get; set; }
        public int UrlImg { get; set; }
        public string? Description { get; set; }
        public Int64 Quantity { get; set; }
    }
}
