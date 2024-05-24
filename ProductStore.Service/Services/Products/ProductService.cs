using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductStore.Data.IRepositories;
using ProductStore.Domain.Configurations;
using ProductStore.Domain.Entities.Products;
using ProductStore.Service.Commons.Exceptions;
using ProductStore.Service.Commons.Extensions;
using ProductStore.Service.DTOs.Products;
using ProductStore.Service.Interfaces.Categories;
using ProductStore.Service.Interfaces.Products;

namespace ProductStore.Service.Services.Products;

public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private readonly ICategoryService _categoryService;
    private readonly IRepository<Product> _productRepository;

    public ProductService(
        IRepository<Product> productRepository,
        IMapper mapper,
        ICategoryService categoryService)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _categoryService = categoryService;
    }

    public async Task<ProductForResultDto> CreateAsync(ProductForCreationDto productForCreationDto)
    {
        var product = await _productRepository.SelectAll()
            .Where(p => p.Name == productForCreationDto.Name)
            .FirstOrDefaultAsync();

        var categoryTask = _categoryService.RetrieveByIdAsync(productForCreationDto.CategoryId);
        if (product is not null)
        {
            if (productForCreationDto.Action)
            {
                if (product.Price != productForCreationDto.Price)
                {
                    product.Price = (product.Price + productForCreationDto.Price) / 2;
                    product.StockQuantity += productForCreationDto.StockQuantity;
                    product.UpdatedAt = DateTime.UtcNow;
                    await _productRepository.UpdateAsync(product);
                    throw new CustomException(200, "The product was added to the stock product as it was in stock.");
                }
                else
                {
                    product.StockQuantity += productForCreationDto.StockQuantity;
                    product.UpdatedAt = DateTime.UtcNow;
                    await _productRepository.UpdateAsync(product);
                    throw new CustomException(200, "The product was added to the stock product as it was in stock.");
                }
            }
            else
            {
                if (product.Price != productForCreationDto.Price)
                {
                    product.Price = productForCreationDto.Price;
                    product.StockQuantity += productForCreationDto.StockQuantity;
                    product.UpdatedAt = DateTime.UtcNow;
                    await _productRepository.UpdateAsync(product);
                    throw new CustomException(200, "The product was added to the stock product as it was in stock.");
                }
                else
                {
                    product.StockQuantity += productForCreationDto.StockQuantity;
                    product.UpdatedAt = DateTime.UtcNow;
                    await _productRepository.UpdateAsync(product);
                    throw new CustomException(200, "The product was added to the stock product as it was in stock.");
                }
            }
        }

        var mappedProduct = _mapper.Map<Product>(productForCreationDto);
        mappedProduct.CreatedAt = DateTime.UtcNow;

        var result = await _productRepository.InsertAsync(mappedProduct);

        return _mapper.Map<ProductForResultDto>(result);
    }

    public async Task<ProductForResultDto> ModifyAsync(long id, ProductForUpdateDto productForUpdateDto)
    {
        var product = await _productRepository.SelectAll()
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();

        var category = await _categoryService.RetrieveByIdAsync(productForUpdateDto.CategoryId);

        if (product is null)
            throw new CustomException(404, "Product is not found.");


        var mapped = _mapper.Map(productForUpdateDto, product);
        mapped.UpdatedAt = DateTime.UtcNow;

        await _productRepository.UpdateAsync(mapped);

        return _mapper.Map<ProductForResultDto>(mapped);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var product = await _productRepository.SelectAll()
            .Where(p => p.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (product is null)
            throw new CustomException(404, "Product is not found.");

        return await _productRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<ProductForResultDto>> SelectAllAsync(PaginationParams @params)
    {
        var products = await _productRepository.SelectAll()
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<ProductForResultDto>>(products);
    }

    public async Task<ProductForResultDto> SelectByIdAsync(long id)
    {
        var product = await _productRepository.SelectAll()
             .Where(p => p.Id == id)
             .AsNoTracking()
             .FirstOrDefaultAsync();

        if (product is null)
            throw new CustomException(404, "Product is not found.");

        return _mapper.Map<ProductForResultDto>(product);
    }
}
