using Microsoft.AspNetCore.Mvc;
using CRNProductAssessment.Application.Services;
using CRNProductAssessment.Domain.Entities;
using CRNProductAssessment.Application.DTOs;
using FluentValidation;

namespace CRNProductAssessment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IValidator<CreateProductDto> _createValidator;
        private readonly IValidator<UpdateProductDto> _updateValidator;

        public ProductController(
            IProductService service,
            IValidator<CreateProductDto> createValidator,
            IValidator<UpdateProductDto> updateValidator)
        {
            _service = service;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        //[HttpPost]
        //public async Task<IActionResult> Create(CreateProductDto dto)
        //{
        //    var result = await _service.CreateAsync(dto);

        //    return Ok(result);
        //}

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            var validationResult = await _createValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage));

            var result = await _service.CreateAsync(dto);

            return Ok(result);
        }
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int id, UpdateProductDto dto)
        //{
        //    var result = await _service.UpdateAsync(id, dto);

        //    if (!result)
        //        return NotFound();

        //    return Ok("Updated Successfully");
        //}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateProductDto dto)
        {
            var validationResult = await _updateValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage));

            var result = await _service.UpdateAsync(id, dto);

            if (!result)
                return NotFound("Product not found.");

            return Ok("Updated Successfully");
        }
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        var result = await _service.DeleteAsync(id);

        //        if (!result)
        //            return NotFound("Product not found.");

        //        return Ok("Deleted Successfully");
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);

            if (!result)
                return NotFound("Product not found.");

            return Ok("Deleted Successfully");
        }
    }
}
