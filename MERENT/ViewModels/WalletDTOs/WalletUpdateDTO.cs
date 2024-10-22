using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.WalletDTOs
{
    public class WalletUpdateDTO
    {
        public string Id { get; set; }
        public Int64? Cash { get; set; }
        public string? WalletType { get; set; }
    }
}
