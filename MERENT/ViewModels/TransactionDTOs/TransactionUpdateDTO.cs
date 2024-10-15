using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.TransactionDTOs
{
    public class TransactionUpdateDTO
    {
        public int Id { get; set; }
        public int WalletId { get; set; }
        public Int64 TotalAmount { get; set; }
        public string? PaymentType { get; set; }
    }
}
