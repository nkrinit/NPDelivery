using Mediator;

using Microsoft.EntityFrameworkCore;

using NPDelivery.Data;
using NPDelivery.Features.Customers;
using NPDelivery.Features.Orders;
using NPDelivery.Features.Products;
using NPDelivery.Features.Stores;
using NPDelivery.PipelineBehaviors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Scoped);
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

AddMappers(builder);

// Add database
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(connection);
    options.EnableSensitiveDataLogging();
});

var app = builder.Build();

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

static void AddMappers(WebApplicationBuilder builder)
{
    builder.Services.AddSingleton<OrderMapper>();
    builder.Services.AddSingleton<ProductMapper>();
    builder.Services.AddSingleton<StoreMapper>();
    builder.Services.AddSingleton<CustomerMapper>();
}