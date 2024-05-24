using AutoMapper;
using ProductStore.Domain.Entities.Categories;
using ProductStore.Domain.Entities.Orders;
using ProductStore.Domain.Entities.Products;
using ProductStore.Domain.Entities.Reports;
using ProductStore.Domain.Entities.Users;
using ProductStore.Service.DTOs.Categories;
using ProductStore.Service.DTOs.CategoryDTOs;
using ProductStore.Service.DTOs.OrderItems;
using ProductStore.Service.DTOs.Orders;
using ProductStore.Service.DTOs.Products;
using ProductStore.Service.DTOs.Reports;
using ProductStore.Service.DTOs.Users;

namespace ProductStore.Service.Meppers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        // Users
        CreateMap<User, UserForResultDto>().ReverseMap();
        CreateMap<User, UserForUpdateDto>().ReverseMap();
        CreateMap<User, UserForCreationDto>().ReverseMap();
        CreateMap<User, UserForChangePasswordDto>().ReverseMap();

        // Categories
        CreateMap<Category, CategoryForResultDto>().ReverseMap();
        CreateMap<Category, CategoryForUpdateDto>().ReverseMap();
        CreateMap<Category, CategoryForCreationDto>().ReverseMap();

        // Reports
        CreateMap<Report, ReportForCreationDto>().ReverseMap();
        CreateMap<Report, ReportForResultDto>().ReverseMap();
        CreateMap<Report, ReportForUpdateDto>().ReverseMap();

        // Orders
        CreateMap<Order, OrderForCreationDto>().ReverseMap();
        CreateMap<Order, OrderForResultDto>().ReverseMap();
        CreateMap<Order, OrderForUpdateDto>().ReverseMap();

        // OrderItems
        CreateMap<OrderItem, OrderItemForCreationDto>().ReverseMap();
        CreateMap<OrderItem, OrderItemForResultDto>().ReverseMap();
        CreateMap<OrderItem, OrderItemForUpdateDto>().ReverseMap();

        // Products
        CreateMap<Product, ProductForCreationDto>().ReverseMap();
        CreateMap<Product, ProductForResultDto>().ReverseMap();
        CreateMap<Product, ProductForUpdateDto>().ReverseMap();
    }
}
