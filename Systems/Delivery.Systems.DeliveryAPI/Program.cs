using Delivery.Domain.Context;
using Delivery.Domain.Context.Setup;
using Delivery.Domain.Seeder.Seeds;
using Delivery.Services.OrderService;
using Delivery.Systems.DeliveryAPI.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddOrderService();
builder.Services.AddAppDbContext(builder.Configuration);
builder.Services.AddAppAutoMapper();

builder.AddAppLogger(builder.Configuration);
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.UseHttpsRedirection();

DbInitializer.Execute(app.Services);

DbSeeder.Execute(app.Services);

app.Run();