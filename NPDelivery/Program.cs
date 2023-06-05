using System.Text.Json.Serialization;

using Mediator;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

using NPDelivery.Auth;
using NPDelivery.Data;
using NPDelivery.Features.Couriers;
using NPDelivery.Features.Customers;
using NPDelivery.Features.Orders;
using NPDelivery.Features.Products;
using NPDelivery.Features.Stores;
using NPDelivery.PipelineBehaviors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Scoped);
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

// Add policies
builder.Services.AddAuthorization(o =>
{
    o.AddPolicy(PolicyNames.Customer, p => p
        .RequireClaim(CustomClaimTypes.Role, ApplicationRole.Customer.ToString()));

    o.AddPolicy(PolicyNames.Courier, p => p
        .RequireClaim(CustomClaimTypes.Role, ApplicationRole.Courier.ToString()));

    o.AddPolicy(PolicyNames.StoreKeeper, p => p
        .RequireClaim(CustomClaimTypes.Role, ApplicationRole.StoreKeeper.ToString()));

    o.AddPolicy(PolicyNames.Admin, p => p
        .RequireClaim(CustomClaimTypes.Role, ApplicationRole.Admin.ToString()));

    o.AddPolicy(PolicyNames.Support, p => p
        .RequireClaim(CustomClaimTypes.Role, ApplicationRole.Support.ToString()));
});

// N.B. to use Jwt/"Bearer" auth scheme, go with JwtBearerDefaults.AuthenticationScheme + .AddJwtBearer() afterwards
builder.Services.AddAuthentication(o =>
{
    o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    o.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    o.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
{
    o.AccessDeniedPath = null;
    o.LoginPath = null;
    o.LogoutPath = null;
    o.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask; 
    };
    o.Events.OnRedirectToAccessDenied = context =>
    {
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        return Task.CompletedTask;
    };
    //o.Events.OnRedirectToLogout = context =>
    //{
    //    context.RedirectUri = "/Login";
    //    return Task.CompletedTask;
    //};
});

AddMappers(builder);

// Add database
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(connection);
    options.EnableSensitiveDataLogging();
});

var app = builder.Build();

// N.B. Below are middlewares

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Auth
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

static void AddMappers(WebApplicationBuilder builder)
{
    builder.Services.AddSingleton<OrderMapper>();
    builder.Services.AddSingleton<ProductMapper>();
    builder.Services.AddSingleton<StoreMapper>();
    builder.Services.AddSingleton<CustomerMapper>();
    builder.Services.AddSingleton<CourierMapper>();
}