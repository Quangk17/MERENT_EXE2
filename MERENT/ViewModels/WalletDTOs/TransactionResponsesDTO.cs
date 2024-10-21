using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.WalletDTOs
{
    public class TransactionResponsesDTO
    {
        public int Id { get; set; }
        public int WalletId { get; set; }
        public string TransactionType { get; set; }
        public long TotalAmount { get; set; }
        public string? Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Status { get; set; }
    }
}
