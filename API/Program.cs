using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//initialising dbcontext server
builder.Services.AddDbContext<DataContext>(opt => 
{
    //connection string
    opt.UseSqlite(builder.Configuration.GetConnectionString("defaultConnections"));
});

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.//Middleware
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
