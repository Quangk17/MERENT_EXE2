﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class Wallets: BaseEntity
    {
        public Int64? cash {  get; set; }
        public int? userID { get; set; }

        //R
        public virtual User User { get; set; }  

    }
}
