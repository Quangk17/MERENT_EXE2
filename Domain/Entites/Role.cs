using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class Role : IdentityRole<int>
    {
        public  string? Name { get; set; }

        //R
        public virtual ICollection<User>? Users { get; set; }

    }
}
