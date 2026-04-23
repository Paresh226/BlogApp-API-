using BlogApp_API_.Models;

namespace BlogApp_API_.Repositories;

public interface IBlogPostRepository
{
    Task<List<BlogPost>> GetAllAsync(CancellationToken cancellationToken);
    Task<List<BlogPost>> GetPublishedAsync(CancellationToken cancellationToken);
    Task<BlogPost?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<BlogPost?> GetPublishedBySlugAsync(string slug, CancellationToken cancellationToken);
    Task<bool> SlugExistsAsync(string slug, Guid? excludingId, CancellationToken cancellationToken);
    Task AddAsync(BlogPost post, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task DeleteAsync(BlogPost post, CancellationToken cancellationToken);
}

