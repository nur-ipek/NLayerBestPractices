using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NLayerBestPractices.Core.Repositories;
using NLayerBestPractices.Core.UnitOfWorks;
using NLayerBestPractices.Data.Repositories;

namespace NLayerBestPractices.Data.UnitOfWorks
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly AppDbContext _context;

        private ProductRepository _productRepository;
        private CategoryRepository _categoryRepository;

        public IProductRepository Products => _productRepository = _productRepository ?? new ProductRepository(_context);
        public ICategoryRepository Categories => _categoryRepository = _categoryRepository ?? new CategoryRepository(_context);
        // ?? -> null ise demek.

        public UnitOfWork(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
