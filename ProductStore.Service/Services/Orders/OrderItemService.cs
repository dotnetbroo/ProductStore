using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductStore.Data.IRepositories;
using ProductStore.Domain.Configurations;
using ProductStore.Domain.Entities.Orders;
using ProductStore.Domain.Entities.Products;
using ProductStore.Domain.Enums;
using ProductStore.Service.Commons.Exceptions;
using ProductStore.Service.Commons.Extensions;
using ProductStore.Service.DTOs.OrderItems;
using ProductStore.Service.Interfaces.Orders;
using ProductStore.Service.Interfaces.Products;
using ProductStore.Service.Interfaces.Users;

namespace ProductStore.Service.Services.Orders;

public class OrderItemService : IOrderItemService
{
    private readonly IRepository<OrderItem> _orderItemRepository;
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<Product> _productRepository; 
    private readonly IMapper _mapper;

    public OrderItemService(IRepository<OrderItem> orderItemRepository,
        IMapper mapper, 
        IRepository<Product> productRepository,
        IRepository<Order> orderRepository)
    {
        _orderItemRepository = orderItemRepository;
        _mapper = mapper;
        _productRepository = productRepository;
        _orderRepository = orderRepository;
    }

    public async Task<OrderItemForResultDto> CreateAsync(OrderItemForCreationDto dto)
    {

        var order = await _orderRepository.SelectAll()
            .Where(o => o.Id == dto.OrderId)
            .FirstOrDefaultAsync();
        if (order is null)
            throw new CustomException(404, "Order is not found.");

        var product = await _productRepository.SelectAll()
            .Where(p => p.Id == dto.ProductId)
            .FirstOrDefaultAsync();
        if (product is null)
            throw new CustomException(404, "Product is not found.");

        var orderItem = _mapper.Map<OrderItem>(dto);
        orderItem.CreatedAt = DateTime.UtcNow;
        orderItem.UnitPrice = product.Price;

        order.TotalPrice += (dto.Quantity * product.Price);
        order.Status = OrderStatus.Processing;
        order.UpdatedAt = DateTime.UtcNow;

        await _orderRepository.UpdateAsync(order);

        await _orderItemRepository.InsertAsync(orderItem);

        return _mapper.Map<OrderItemForResultDto>(orderItem);
    }

    public async Task<OrderItemForResultDto> ModifyAsync(long id, OrderItemForUpdateDto dto)
    {
        var orderItem = await _orderItemRepository.SelectAll()
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();
        if (orderItem is null)
            throw new CustomException(404, "OrderItem not found");

        if(orderItem.Quantity != dto.Quantity)
        {
            var order = await _orderRepository.SelectAll()
            .Where(o => o.Id == orderItem.OrderId)
            .FirstOrDefaultAsync();

            order.TotalPrice -= orderItem.Quantity * orderItem.UnitPrice;
            await _orderRepository.UpdateAsync(order);

            order.TotalPrice += dto.Quantity * orderItem.UnitPrice;
            await _orderRepository.UpdateAsync(order);
        }

        _mapper.Map(dto, orderItem);
        orderItem.UpdatedAt = DateTime.UtcNow;

        await _orderItemRepository.UpdateAsync(orderItem);

        return _mapper.Map<OrderItemForResultDto>(orderItem);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var orderItem = await _orderItemRepository.SelectAll()
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();
        if (orderItem is null)
            throw new CustomException(404, "OrderItem not found");
        
        var order = await _orderRepository.SelectAll()
            .Where(o => o.Id == orderItem.OrderId)
            .FirstOrDefaultAsync();
        if(order is not null)
            order.TotalPrice -= orderItem.UnitPrice * orderItem.Quantity;

        await _orderItemRepository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<OrderItemForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var orderItems = await _orderItemRepository.SelectAll()
                                                   .AsNoTracking()
                                                   .ToPagedList(@params)
                                                   .ToListAsync();

        return _mapper.Map<IEnumerable<OrderItemForResultDto>>(orderItems);
    }

    public async Task<OrderItemForResultDto> RetrieveByIdAsync(long id)
    {
        var orderItem = await _orderItemRepository.SelectAll()
            .Where(p => p.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (orderItem is null)
            throw new CustomException(404, "OrderItem not found");

        return _mapper.Map<OrderItemForResultDto>(orderItem);
    }
}
