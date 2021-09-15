using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayerBestPractices.Core.Models;

namespace NLayerBestPractices.Data.Seeds
{
    class ProductSeed : IEntityTypeConfiguration<Product>
    {
        private readonly int[] _ids; //Category ıd'leri geliyor...
        public ProductSeed(int[] ids)
        {
            _ids = ids;
        }
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(new Product { Id = 1, Name = "Kurşun Kalem", Price = 15.50m, Stock = 300, CategoryId = _ids[0] });
            builder.HasData(new Product { Id = 2, Name = "Pilot Kalem", Price = 30.00m, Stock = 100, CategoryId = _ids[0] });
            builder.HasData(new Product { Id = 3, Name = "Sırt Çantası", Price = 100.00m, Stock = 500, CategoryId = _ids[1] });
            builder.HasData(new Product { Id = 4, Name = "Beslenme Çantası", Price = 80.00m, Stock = 600, CategoryId = _ids[1] });
        }
    }
}
