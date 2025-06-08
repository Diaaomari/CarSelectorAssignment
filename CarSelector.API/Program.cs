using CarSelector.Application.Interfaces;
using CarSelector.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var nhtsaBaseUrl = Environment.GetEnvironmentVariable("NhtsaBaseUrl") ?? "https://vpic.nhtsa.dot.gov/api/vehicles/";

builder.Services.AddHttpClient<INhtsaService, NhtsaService>(client =>
{
    client.BaseAddress = new Uri(nhtsaBaseUrl);
});
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
