using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DbapiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL")));

builder.Services.AddControllers().AddJsonOptions(option => { option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });

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
