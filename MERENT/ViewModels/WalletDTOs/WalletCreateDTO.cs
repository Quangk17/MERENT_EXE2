using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.WalletDTOs
{
    public class WalletCreateDTO
    {
        public int UserId { get; set; }
        public long? Cash { get; set; }
    }
}
