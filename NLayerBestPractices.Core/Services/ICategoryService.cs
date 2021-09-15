using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NLayerBestPractices.Core.Models;

namespace NLayerBestPractices.Core.Services
{
    interface ICategoryService: IService<Category>
    {
        Task<Category> GetWithProductsByIdAsync(int categoryId);

    }
}
