using apbd_kolokwium2.Services;
using Microsoft.EntityFrameworkCore;
using AppContext = apbd_kolokwium2.Data.AppContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<AppContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddScoped<IDbService, DbService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();


/*REMEMBER TO USE*/
/*
 * 1. dotnet ef migrations add Init
 * 2. dotnet ef database update (albo drop jak coś nie działa)
 * 
 */

app.Run();