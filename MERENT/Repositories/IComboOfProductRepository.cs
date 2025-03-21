﻿using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IComboOfProductRepository: IGenericRepository<ComboOfProduct>
    {
        Task<List<Combo>> GetAllCombosWithProductsAsync();
        Task<Combo?> GetComboWithProductsByIdAsync(int comboId);
    }
}
