using Microsoft.EntityFrameworkCore;
using Practice.Application.Interfaces;
using Practice.Application.Services;
using Practice.Persistence;
using Practice.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PracticeDbContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(PracticeDbContext)));
    });

builder.Services.AddScoped<IPracticeService, PracticeService>();
builder.Services.AddScoped<IPracticeRepository, PracticeRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
