namespace Profile.Infrastructure;

public class DataContext : IdentityDbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<Owner>? OwnerInfo { get; set; }
    public DbSet<Projects>? Projects { get; set; }
    public DbSet<OwnerAbout> OwnerAbout { get; set; }
}
