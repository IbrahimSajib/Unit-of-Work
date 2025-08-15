using Microsoft.EntityFrameworkCore;
using System;
using UnitOfWork.Core.Entities;
using UnitOfWork.Core.Interfaces;
using UnitOfWork.Infrastructure.Data;
using UnitOfWork.Infrastructure.Repositories;
using UnitOfWork.Services.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// EF Core with In-Memory DB
builder.Services.AddDbContext<DataContext>(options =>
    options.UseInMemoryDatabase("UnitOfWorkDb"));

// Dependency Injection
builder.Services.AddScoped<IUnitOfWork, UnitOfWork.Infrastructure.Repositories.UnitOfWork>();
builder.Services.AddScoped<UserService>();


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    context.Users.AddRange(
        new User { Name = "Ibrahim", Email = "ibrahim@abc.com" },
        new User { Name = "Sajib", Email = "sajib@abc.com" }
    );
    context.SaveChanges();
}


app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});


app.Run();
