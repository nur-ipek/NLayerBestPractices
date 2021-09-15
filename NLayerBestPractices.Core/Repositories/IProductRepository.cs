using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NLayerBestPractices.Core.Models;

namespace NLayerBestPractices.Core.Repositories
{
    //API Repositoryile doğrudan haberleşmemeli...
    public interface IProductRepository: IRepository<Product>
    {
        Task<Product> GetWithCategoryByIdAsync(int productId);
    }
}
