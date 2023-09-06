using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext : DbContext
{
    //constructor
    public DataContext(DbContextOptions options) : base(options)
    {
        
    }
    //Users is table name column name inside appuser class
    public DbSet <AppUser> Users { get; set; }
}
