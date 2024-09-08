using Microsoft.EntityFrameworkCore;
using RateMe.Persistence;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<RateMeDbContext>(options =>
{
   options.UseNpgsql(configuration.GetConnectionString(nameof(RateMeDbContext)));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();