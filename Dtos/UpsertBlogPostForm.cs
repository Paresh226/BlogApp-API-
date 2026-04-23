using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp_API_.Dtos;

public sealed class UpsertBlogPostForm
{
    [Required]
    [FromForm(Name = "title")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [FromForm(Name = "slug")]
    public string Slug { get; set; } = string.Empty;

    [FromForm(Name = "excerpt")]
    public string Excerpt { get; set; } = string.Empty;

    [FromForm(Name = "content")]
    public string Content { get; set; } = string.Empty;

    [FromForm(Name = "published")]
    public bool Published { get; set; }

    [FromForm(Name = "removeImage")]
    public bool RemoveImage { get; set; }

    [FromForm(Name = "image")]
    public IFormFile? Image { get; set; }
}

