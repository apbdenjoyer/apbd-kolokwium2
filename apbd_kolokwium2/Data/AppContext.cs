using Microsoft.EntityFrameworkCore;

namespace apbd_kolokwium2.Data;

public class AppContext : DbContext
{
    public AppContext()
    { }

    public AppContext(DbContextOptions<AppContext> options) : base(options)
    { }

    /*DbSets*/
    /*private DbSet<EntityOne> EntityOnes { get; set; }
     private DbSet<EntityTwo> EntityTwos { get; set; }*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        /*create entities*/

        /*modelBuilder.Entity<EntityOne>(e =>
    {
        e.ToTable("Entity One");
        e.HasKey(e => e.Id);
        e.Property(e => e.Name).HasMaxLength(100);

        e.HasData(new List<EntityOne>
        {
            new EntityOne() { Id = 1, Name = "ABC" }
        });
    });

    modelBuilder.Entity<EntityTwo>(e =>
    {
        e.ToTable("Entity Two");
        e.HasKey(e => e.Id);
        e.Property(e => e.Value);

        e.HasOne(e => e.EntityOne)
            .WithMany(e => e.EntityTwos)
            .HasForeignKey(e => e.EntityOneId)
            .OnDelete(DeleteBehavior.Cascade);

        e.HasData(new List<EntityTwo>
        {
            new EntityTwo() { Id = 1, Value = 5, EntityOneId = 1 }
        });
    });*/
    }
}