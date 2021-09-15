using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using NLayerBestPractices.Core.Models;
using NLayerBestPractices.Data.Configurations;
using NLayerBestPractices.Data.Seeds;

namespace NLayerBestPractices.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base (options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        //veri tabanında tablolar oluşmadan çalışacak metot.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());

            modelBuilder.ApplyConfiguration(new ProductSeed(new int[] { 1, 2}));
            modelBuilder.ApplyConfiguration(new CategorySeed(new int[] { 1, 2 }));
        }
    }
}
