using System.Collections.Generic;
using AutoMapper;
using Valtech.ProductForm.Context;
using Valtech.ProductForm.Models;

namespace Valtech.ProductForm.Mappings
{
    public static class MappingProfile
    {
        public static MapperConfiguration InitializeAutoMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryVm>();
                cfg.CreateMap<CategoryVm, Category>();

                cfg.CreateMap<Product, ProductVm>();
                cfg.CreateMap<ProductVm, Product>();

                cfg.CreateMap<IEnumerable<Category>, IEnumerable<CategoryVm>>();
                cfg.CreateMap<IEnumerable<Product>, IEnumerable<ProductVm>>();
            });

            return config;
        }
    }
}