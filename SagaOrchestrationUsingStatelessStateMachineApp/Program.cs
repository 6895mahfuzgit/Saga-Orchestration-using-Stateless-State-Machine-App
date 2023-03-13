using SagaOrchestrationUsingStatelessStateMachineApp.Managers;
using SagaOrchestrationUsingStatelessStateMachineApp.Proxies;
using SagaOrchestrationUsingStatelessStateMachineApp.Proxies.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient("Order", c => c.BaseAddress = new Uri("http://localhost:9200"));
builder.Services.AddHttpClient("Inventory", c => c.BaseAddress = new Uri("http://localhost:17426"));
builder.Services.AddHttpClient("Transport", c => c.BaseAddress = new Uri("http://localhost:31999"));
builder.Services.AddSingleton<ITranspostProxy, TranspostProxy>();
builder.Services.AddSingleton<IOrderProxy, OrderProxy>();
builder.Services.AddSingleton<IInventoryProxy, InventoryProxy>();
builder.Services.AddSingleton<IOrderManager, OrderManager>();





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
