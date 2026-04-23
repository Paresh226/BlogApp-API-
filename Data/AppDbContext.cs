using BlogApp_API_.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApp_API_.Data;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<BlogPost> BlogPosts => Set<BlogPost>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BlogPost>(entity =>
        {
            entity.HasIndex(p => p.Slug).IsUnique();
            entity.Property(p => p.Title).HasMaxLength(200);
            entity.Property(p => p.Slug).HasMaxLength(220);
            entity.Property(p => p.Excerpt).HasMaxLength(500);
            entity.Property(p => p.ImageUrl).HasMaxLength(500);
        });
    }
}

