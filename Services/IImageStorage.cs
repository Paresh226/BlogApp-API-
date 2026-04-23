namespace BlogApp_API_.Services;

public interface IImageStorage
{
    Task<string> SaveAsync(IFormFile file, CancellationToken cancellationToken);
    Task DeleteIfLocalAsync(string imageUrl, CancellationToken cancellationToken);
}

