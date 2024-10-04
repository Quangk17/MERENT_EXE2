using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.ProductDTOs
{
    public class ProductUpdateDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ProductType { get; set; }
        public string? URLCenter { get; set; }
        public string? URLLeft { get; set; }
        public string? URLRight { get; set; }
        public string? URLSide { get; set; }
        public Int64? Price { get; set; }
    }
}
