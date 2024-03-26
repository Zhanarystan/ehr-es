using API.Core;
using Microsoft.EntityFrameworkCore;

namespace API.Persistence;
public class DataContext : DbContext
{
    public DbSet<EventEntity> Events { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

}