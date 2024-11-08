using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.ServiceOrderDTOs
{
    public class ServiceOrderDTOs
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ServiceDate { get; set; }
        public Int64 TotalAmount { get; set; }
        public int? UserId { get; set; }
        public OrderStatusTypeEnums? StatusOrder { get; set; }
    }
}
