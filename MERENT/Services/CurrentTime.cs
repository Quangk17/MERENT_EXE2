using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CurrentTime 
    {
        public DateTime GetCurrentTime() => DateTime.UtcNow;
    }
}
