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

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
