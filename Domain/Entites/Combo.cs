using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class Combo: BaseEntity
    {
        public string? Name {  get; set; }
        public string? Description { get; set; }
        //R
        public virtual ICollection<ComboOfProduct>? Products { get; set; }   

    }
}
