using apbd_kolokwium2.Models;
using Microsoft.EntityFrameworkCore;

namespace apbd_kolokwium2.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext()
    { }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    { }

    /*DbSets*/
    public DbSet<Item> Items { get; set; }
    public DbSet<Backpack> Backpacks { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<CharacterTitle> CharacterTitles { get; set; }
    public DbSet<Title> Titles { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        /*create entities*/
        modelBuilder.Entity<Item>(entity =>
        {
            entity.ToTable("items");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasData(new List<Item>
            {
                new Item() { Id = 1, Name = "Item1", Weight = 10 }
            });
        });
        
        modelBuilder.Entity<Character>(entity =>
        {
            entity.ToTable("characters");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(120);
            
            entity.HasData(new List<Character>
            {
                new Character() { Id = 1, FirstName = "John", LastName = "Yakuza", CurrentWeight = 0, MaxWeight = 200}
            });
        });
        
        modelBuilder.Entity<Title>(entity =>
        {
            entity.ToTable("titles");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(100);

            
            entity.HasData(new List<Title>
            {
                new Title() { Id = 1, Name = "Title1"}
            });
        });
        
        modelBuilder.Entity<CharacterTitle>(entity =>
        {
            entity.ToTable("character_titles");
            entity.HasKey(e => new {e.CharacterId, e.TitleId});

            entity.HasOne(e => e.Character)
                .WithMany(e => e.CharacterTitles)
                .HasForeignKey(e => e.CharacterId);

            entity.HasOne(e => e.Title)
                .WithMany(e => e.CharacterTitles)
                .HasForeignKey(e => e.TitleId);
            
            entity.HasData(new List<CharacterTitle>
            {
                new CharacterTitle() { CharacterId = 1, TitleId = 1, AcquiredAt = DateTime.Now}
            });
        });

        modelBuilder.Entity<Backpack>(entity =>
        {
            entity.ToTable("backpacks");
            entity.HasKey(e => new {e.CharacterId, e.ItemId});

            entity.HasOne(e => e.Character)
                .WithMany(e => e.Backpacks)
                .HasForeignKey(e => e.CharacterId);

            entity.HasOne(e => e.Item)
                .WithMany(e => e.Backpacks)
                .HasForeignKey(e => e.ItemId);
            
            entity.HasData(new List<Backpack>
            {
                new Backpack() {CharacterId = 1, ItemId = 1, Amount = 2}
            });
        });
    }
}