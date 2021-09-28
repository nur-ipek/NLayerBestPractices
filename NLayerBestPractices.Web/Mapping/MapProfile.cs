using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NLayerBestPractices.Core.Models;
using NLayerBestPractices.Web.DTOs;

namespace NLayerBestPractices.Web.Mapping
{
    public class MapProfile: Profile
    {
        public MapProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();

            CreateMap<CategoryWithProductDto, Category>();
            CreateMap<Category, CategoryWithProductDto>();

            CreateMap<ProductWithCategoryDto, Product>();
            CreateMap<Product, ProductWithCategoryDto>();
        }
    }
}
