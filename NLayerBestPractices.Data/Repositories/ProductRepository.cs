using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NLayerBestPractices.Core.Models;
using NLayerBestPractices.Core.Repositories;

namespace NLayerBestPractices.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository // Buraya Repository'den gelen bir constructor var.
    {
        private AppDbContext appDbContext { get => _context as AppDbContext; }
        public ProductRepository(AppDbContext context): base(context)
        {

        }
        public async Task<Product> GetWithCategoryByIdAsync(int productId)
        {
            return await appDbContext.Products.Include(x => x.Category).SingleOrDefaultAsync(x => x.Id == productId);
        }
    }
}
