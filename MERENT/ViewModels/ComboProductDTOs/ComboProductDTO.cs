using Application.ViewModels.ProductDTOs;
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

    public class ComboDetailsDTO
    {
        public int ComboID { get; set; }
        public string? ComboName { get; set; }
        public string? Description { get; set; }
        public string? UrlImg { get; set; }
        public List<ProductComboDTO> Products { get; set; } = new List<ProductComboDTO>();
        public long TotalPrice { get; set; } // Tổng giá của combo
    }

    public class ProductComboDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ProductType { get; set; }
        public long? Price { get; set; }
        public long Quantity { get; set; } // Số lượng của sản phẩm trong combo
        public string? URLCenter { get; set; }
        public string? URLLeft { get; set; }
        public string? URLRight { get; set; }
        public string? URLSide { get; set; }
    }

}
