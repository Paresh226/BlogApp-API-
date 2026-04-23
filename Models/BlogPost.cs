using System.ComponentModel.DataAnnotations;

namespace BlogApp_API_.Models;

public sealed class BlogPost
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(220)]
    public string Slug { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Excerpt { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    [MaxLength(500)]
    public string ImageUrl { get; set; } = string.Empty;

    public DateTime? PublishedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}

