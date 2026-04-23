using BlogApp_API_.Data;
using BlogApp_API_.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApp_API_.Repositories;

public sealed class BlogPostRepository : IBlogPostRepository
{
    private readonly AppDbContext _db;

    public BlogPostRepository(AppDbContext db)
    {
        _db = db;
    }

    public Task<List<BlogPost>> GetAllAsync(CancellationToken cancellationToken) =>
        _db.BlogPosts
            .OrderByDescending(p => p.UpdatedAt)
            .ToListAsync(cancellationToken);

    public Task<List<BlogPost>> GetPublishedAsync(CancellationToken cancellationToken) =>
        _db.BlogPosts
            .Where(p => p.PublishedAt != null)
            .OrderByDescending(p => p.PublishedAt)
            .ToListAsync(cancellationToken);

    public Task<BlogPost?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        _db.BlogPosts.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

    public Task<BlogPost?> GetPublishedBySlugAsync(string slug, CancellationToken cancellationToken) =>
        _db.BlogPosts.FirstOrDefaultAsync(p => p.Slug == slug && p.PublishedAt != null, cancellationToken);

    public Task<bool> SlugExistsAsync(string slug, Guid? excludingId, CancellationToken cancellationToken) =>
        _db.BlogPosts.AnyAsync(
            p => p.Slug == slug && (excludingId == null || p.Id != excludingId),
            cancellationToken
        );

    public Task AddAsync(BlogPost post, CancellationToken cancellationToken) =>
        _db.BlogPosts.AddAsync(post, cancellationToken).AsTask();

    public Task SaveChangesAsync(CancellationToken cancellationToken) =>
        _db.SaveChangesAsync(cancellationToken);

    public Task DeleteAsync(BlogPost post, CancellationToken cancellationToken)
    {
        _db.BlogPosts.Remove(post);
        return Task.CompletedTask;
    }
}

