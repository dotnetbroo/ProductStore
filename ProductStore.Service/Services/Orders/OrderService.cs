using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductStore.Data.IRepositories;
using ProductStore.Domain.Configurations;
using ProductStore.Domain.Entities.Orders;
using ProductStore.Domain.Enums;
using ProductStore.Service.Commons.Exceptions;
using ProductStore.Service.Commons.Extensions;
using ProductStore.Service.DTOs.Orders;
using ProductStore.Service.Interfaces.Orders;

namespace ProductStore.Service.Services.Orders;

public class OrderService : IOrderService
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IMapper _mapper;

    public OrderService(IRepository<Order> orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<OrderForResultDto> CreateAsync(OrderForCreationDto dto)
    {
        var order = _mapper.Map<Order>(dto);
        order.Status = OrderStatus.New;
        await _orderRepository.InsertAsync(order);
        return _mapper.Map<OrderForResultDto>(order);
    }

    public async Task<OrderForResultDto> ModifyAsync(long id, OrderForUpdateDto dto)
    {
        var order = await _orderRepository.SelectAll()
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();
        if (order is null)
            throw new CustomException(404, "Order not found");

        _mapper.Map(dto, order);
        order.UpdatedAt = DateTime.UtcNow;

        await _orderRepository.UpdateAsync(order);

        return _mapper.Map<OrderForResultDto>(order);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var order = await _orderRepository.SelectByIdAsync(id);
        if (order is null)
            throw new CustomException(404, "Order not found");

        await _orderRepository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<OrderForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var orders = await _orderRepository.SelectAll()
                                           .Include(p => p.OrderItems)
                                           .AsNoTracking()
                                           .ToPagedList(@params)
                                           .ToListAsync();

        return _mapper.Map<IEnumerable<OrderForResultDto>>(orders);
    }

    public async Task<OrderForResultDto> RetrieveByIdAsync(long id)
    {
        var order = await _orderRepository.SelectAll()
            .Where(p => p.Id == id)
            .Include(i => i.OrderItems)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (order is null)
            throw new CustomException(404, "Order not found");

        return _mapper.Map<OrderForResultDto>(order);
    }
}
