﻿using AutoMapper;
using ETicaretWebApi.Application.Operations.BrandOperations.Commands.CreateBrand;
using ETicaretWebApi.Application.Operations.BrandOperations.Queries.GetBrands;

using ETicaretWebApi.Application.Operations.CategoryOperations.Commands.CreateCategory;
using ETicaretWebApi.Application.Operations.CategoryOperations.Queries.GetCategories;
using ETicaretWebApi.Application.Operations.MainPageCategoryOperations.Commands.CreateMainPageCategory;
using ETicaretWebApi.Application.Operations.MainPageCategoryOperations.Queries.GetMainPageCategories;
using ETicaretWebApi.Application.Operations.OrderOperations;
using ETicaretWebApi.Application.Operations.ProductCategoryOperations.Commands.CreateProductCategory;
using ETicaretWebApi.Application.Operations.ProductOperations.Queries.GetProduct;
using ETicaretWebApi.Application.Operations.ProductOperations.Queries.GetProducts;
using ETicaretWebApi.Application.Operations.SliderOperations.Commands.CreateSlider;
using ETicaretWebApi.Application.Operations.UserOperations.Queries.CreateUser;
using ETicaretWebApi.Application.ProductOperations.Commands.CreateProduct;
using ETicaretWebApi.Entitites;

namespace ETicaretWebApi.Common
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            // for product
            CreateMap<CreateProductModel, Product>();
            CreateMap<Product, ProductViewModel>();
            CreateMap<Product, ProductsViewModel>();
            // for category
            CreateMap<CreateCategoryModel, Category>();
            CreateMap<Category, CategoriesViewModel>();
            // for productcategory
            CreateMap<CreateProductCategoryModel, ProductCategory>();
            // for mainpagecategory
            CreateMap<MainPageCategory, MainPageCategoryViewModel>();
            CreateMap<CreateMainPageCategoryModel, MainPageCategory>();
            // for brand
            CreateMap<Brand, BrandsViewModel>();
            CreateMap<CreateBrandModel, Brand>();
            // for user
            CreateMap<CreateUserModel, User>().ForMember(model => model.Password, opt => opt.Ignore());
            // for slider
            CreateMap<CreateSliderModel, Slider>();
            // for cartItem
            CreateMap<CartItemModel, CartItem>();

        }
    }
}
