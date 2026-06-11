using CRNProductAssessment.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using CRNProductAssessment.Application.Interfaces;
using CRNProductAssessment.Application.Services;
using CRNProductAssessment.Infrastructure.Repositories;
using CRNProductAssessment.API.Middlewares;
using FluentValidation;
using CRNProductAssessment.Application.DTOs;
using CRNProductAssessment.Application.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<IProductRepository, ProductRepository>();//new added Dependency Injection
builder.Services.AddScoped<IProductService, ProductService>();//new added Dependency Injection
builder.Services.AddScoped<IValidator<CreateProductDto>, CreateProductDtoValidator>();
builder.Services.AddScoped<IValidator<UpdateProductDto>, UpdateProductDtoValidator>();

builder.Services.AddControllers();//added new
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();//added new

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();//new added for Middleware 
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
