using BlogApp_API_.Dtos;
using BlogApp_API_.Models;
using BlogApp_API_.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp_API_.Controllers;

[ApiController]
[Route("api/posts")]
public sealed class PostsController : ControllerBase
{
    private readonly IBlogPostRepository _posts;

    public PostsController(IBlogPostRepository posts)
    {
        _posts = posts;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<List<BlogPostSummaryDto>>> List(CancellationToken cancellationToken)
    {
        var items = await _posts.GetPublishedAsync(cancellationToken);
        return Ok(items.Select(MapSummary).ToList());
    }

    [AllowAnonymous]
    [HttpGet("{slug}")]
    public async Task<ActionResult<BlogPostDetailDto>> GetBySlug(string slug, CancellationToken cancellationToken)
    {
        var post = await _posts.GetPublishedBySlugAsync(slug, cancellationToken);
        if (post == null) return NotFound();
        return Ok(MapDetail(post));
    }

    private static BlogPostSummaryDto MapSummary(BlogPost post) => new()
    {
        Id = post.Id,
        Title = post.Title,
        Slug = post.Slug,
        Excerpt = post.Excerpt,
        ImageUrl = post.ImageUrl,
        PublishedAt = post.PublishedAt,
        UpdatedAt = post.UpdatedAt,
    };

    private static BlogPostDetailDto MapDetail(BlogPost post) => new()
    {
        Id = post.Id,
        Title = post.Title,
        Slug = post.Slug,
        Excerpt = post.Excerpt,
        Content = post.Content,
        ImageUrl = post.ImageUrl,
        PublishedAt = post.PublishedAt,
        UpdatedAt = post.UpdatedAt,
    };
}

