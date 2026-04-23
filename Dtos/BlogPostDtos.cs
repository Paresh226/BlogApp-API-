namespace BlogApp_API_.Dtos;

public sealed class BlogPostSummaryDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Excerpt { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public DateTime? PublishedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public sealed class BlogPostDetailDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Excerpt { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public DateTime? PublishedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
