using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NLayerBestPractices.Core.Models;
using NLayerBestPractices.Core.Repositories;

namespace NLayerBestPractices.Data.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private AppDbContext _appDbContext { get => _context as AppDbContext; }
        public CategoryRepository(AppDbContext context): base(context)
        {

        }
        public async Task<Category> GetWithProductsByIdAsync(int categoryId)
        {
            return await _appDbContext.Categories.Include(x => x.Products).SingleOrDefaultAsync(x => x.Id == categoryId);
        }
    }
}
