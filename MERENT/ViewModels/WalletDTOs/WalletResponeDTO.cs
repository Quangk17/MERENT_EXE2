using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.WalletDTOs
{
    public class WalletResponseDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string WalletType { get; set; }
        public long Cash { get; set; }
    }

    public class WithdrawnRequestDTO
    {
        public string BankNote { get; set; }
        public long Amount { get; set; }
    }
}
