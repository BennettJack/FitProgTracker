using fpt_backend.Data.Models.Gym;
using fpt_backend.Data.Models.GymModels;
using Microsoft.EntityFrameworkCore;

namespace fpt_backend.Data;

public class FtpDbContext : DbContext
{
    public FtpDbContext(DbContextOptions<FtpDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<Muscle> Muscles { get; set; }
    
}