using CRNProductAssessment.Application.Interfaces;
using CRNProductAssessment.Domain.Entities;
using CRNProductAssessment.Application.DTOs;

namespace CRNProductAssessment.Application.Services
{
    public class ProductService:IProductService
    {
        private readonly IProductRepository _repository;
        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<ProductResponseDto>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();

            return products.Select(x => new ProductResponseDto
            {
                Id = x.Id,
                ProductName = x.ProductName,
                CreatedBy = x.CreatedBy,
                CreatedOn = x.CreatedOn,
                ModifiedBy = x.ModifiedBy,
                ModifiedOn = x.ModifiedOn
            }).ToList();
        }

        public async Task<ProductResponseDto?> GetByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                return null;

            return new ProductResponseDto
            {
                Id = product.Id,
                ProductName = product.ProductName,
                CreatedBy = product.CreatedBy,
                CreatedOn = product.CreatedOn,
                ModifiedBy = product.ModifiedBy,
                ModifiedOn = product.ModifiedOn
            };
        }

        public async Task<ProductResponseDto> CreateAsync(CreateProductDto dto)
        {
            var product = new Product
            {
                ProductName = dto.ProductName,
                CreatedBy = dto.CreatedBy,
                CreatedOn = DateTime.Now
            };

            await _repository.AddAsync(product);

            return new ProductResponseDto
            {
                Id = product.Id,
                ProductName = product.ProductName,
                CreatedBy = product.CreatedBy,
                CreatedOn = product.CreatedOn
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateProductDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);

            if (existing == null)
                return false;

            existing.ProductName = dto.ProductName;
            existing.ModifiedBy = dto.ModifiedBy;
            existing.ModifiedOn = DateTime.Now;

            return await _repository.UpdateAsync(existing);
        }

        public Task<bool> DeleteAsync(int id)
        {
            return _repository.DeleteAsync(id);
        }
    }
}
