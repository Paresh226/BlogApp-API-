using System.Security.Cryptography;

namespace BlogApp_API_.Services;

public sealed class LocalImageStorage : IImageStorage
{
    private static readonly HashSet<string> AllowedExt = new(StringComparer.OrdinalIgnoreCase)
    {
        ".jpg",
        ".jpeg",
        ".png",
        ".webp",
        ".gif",
    };

    private readonly string _uploadsDir;

    public LocalImageStorage(IWebHostEnvironment env)
    {
        var webRoot = env.WebRootPath;
        if (string.IsNullOrWhiteSpace(webRoot))
        {
            webRoot = Path.Combine(env.ContentRootPath, "wwwroot");
        }

        _uploadsDir = Path.Combine(webRoot, "uploads");
        Directory.CreateDirectory(_uploadsDir);
    }

    public async Task<string> SaveAsync(IFormFile file, CancellationToken cancellationToken)
    {
        if (file.Length <= 0) throw new InvalidOperationException("Empty file.");
        if (file.Length > 5 * 1024 * 1024) throw new InvalidOperationException("File too large (max 5MB).");

        var ext = Path.GetExtension(file.FileName);
        if (string.IsNullOrWhiteSpace(ext) || !AllowedExt.Contains(ext))
        {
            throw new InvalidOperationException("Unsupported image type.");
        }

        var random = RandomNumberGenerator.GetBytes(16);
        var name = Convert.ToHexString(random).ToLowerInvariant();
        var fileName = $"{name}{ext.ToLowerInvariant()}";
        var fullPath = Path.Combine(_uploadsDir, fileName);

        await using var stream = new FileStream(fullPath, FileMode.CreateNew, FileAccess.Write, FileShare.None);
        await file.CopyToAsync(stream, cancellationToken);

        return $"/uploads/{fileName}";
    }

    public Task DeleteIfLocalAsync(string imageUrl, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(imageUrl)) return Task.CompletedTask;
        if (!imageUrl.StartsWith("/uploads/", StringComparison.OrdinalIgnoreCase)) return Task.CompletedTask;

        var fileName = imageUrl["/uploads/".Length..];
        if (string.IsNullOrWhiteSpace(fileName)) return Task.CompletedTask;

        var fullPath = Path.Combine(_uploadsDir, fileName);
        try
        {
            if (File.Exists(fullPath)) File.Delete(fullPath);
        }
        catch
        {
            // best-effort delete
        }

        return Task.CompletedTask;
    }
}

