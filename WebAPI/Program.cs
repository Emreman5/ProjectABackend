using DataAccess.Concrete.Context;
using DataAccess.Concrete.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conf = builder.Configuration;
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MsSqlDbContext>(optionsBuilder =>
    optionsBuilder.UseSqlServer(conf.GetConnectionString("MsSqlConnectionString")));

builder.Services.AddIdentity<CustomUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true).
    AddEntityFrameworkStores<MsSqlDbContext>();

builder.Services.AddScoped<DbContext, MsSqlDbContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


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