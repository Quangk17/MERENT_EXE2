using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.ComboProductDTOs
{
    public class ComboOfProductDTO
    {
        public int ProductID { get; set; }
        public int ComboID { get; set; }
        public string? Description { get; set; }
        public Int64 Quantity { get; set; }
    }

    public class ComboOfProductCreateDTO
    {
        public int ProductID { get; set; }
        public int ComboID { get; set; }
        public string? Description { get; set; }
        public Int64 Quantity { get; set; }
    }

    public class ComboOfProductUpdateDTO
    {
        public string? Description { get; set; }
        public Int64 Quantity { get; set; }
    }

    public class ComboWithProductsDTO
    {
        public int ComboID { get; set; }
        public List<int> ProductIDs { get; set; } = new List<int>();
    }
}
