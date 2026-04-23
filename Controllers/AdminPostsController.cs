using BlogApp_API_.Dtos;
using BlogApp_API_.Models;
using BlogApp_API_.Repositories;
using BlogApp_API_.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp_API_.Controllers;

[ApiController]
[Authorize(Roles = "admin")]
[Route("api/admin/posts")]
 
public sealed class AdminPostsController : ControllerBase
{
    private readonly IBlogPostRepository _posts;
    private readonly IImageStorage _images;

    public AdminPostsController(IBlogPostRepository posts, IImageStorage images)
    {
        _posts = posts;
        _images = images;
    }

    [HttpGet]
    public async Task<ActionResult<List<BlogPostSummaryDto>>> List(CancellationToken cancellationToken)
    {
        var items = await _posts.GetAllAsync(cancellationToken);
        return Ok(items.Select(MapSummary).ToList());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<BlogPostDetailDto>> Get(Guid id, CancellationToken cancellationToken)
    {
        var post = await _posts.GetByIdAsync(id, cancellationToken);
        if (post == null) return NotFound();
        return Ok(MapDetail(post));
    }

    [HttpPost]
    [RequestSizeLimit(6 * 1024 * 1024)]
    public async Task<ActionResult<BlogPostDetailDto>> Create([FromForm] UpsertBlogPostForm form, CancellationToken cancellationToken)
    {
        var normalizedSlug = NormalizeSlug(form.Slug);
        if (await _posts.SlugExistsAsync(normalizedSlug, excludingId: null, cancellationToken))
        {
            return Conflict(new { message = "Slug already exists." });
        }

        var now = DateTime.UtcNow;

        var post = new BlogPost
        {
            Id = Guid.NewGuid(),
            Title = form.Title.Trim(),
            Slug = normalizedSlug,
            Excerpt = (form.Excerpt ?? string.Empty).Trim(),
            Content = (form.Content ?? string.Empty).Trim(),
            CreatedAt = now,
            UpdatedAt = now,
            PublishedAt = form.Published ? now : null,
        };

        if (form.Image != null)
        {
            post.ImageUrl = await _images.SaveAsync(form.Image, cancellationToken);
        }

        await _posts.AddAsync(post, cancellationToken);
        await _posts.SaveChangesAsync(cancellationToken);

        return Ok(MapDetail(post));
    }

    [HttpPut("{id:guid}")]
    [RequestSizeLimit(6 * 1024 * 1024)]
    public async Task<ActionResult<BlogPostDetailDto>> Update(Guid id, [FromForm] UpsertBlogPostForm form, CancellationToken cancellationToken)
    {
        var post = await _posts.GetByIdAsync(id, cancellationToken);
        if (post == null) return NotFound();

        var normalizedSlug = NormalizeSlug(form.Slug);
        if (await _posts.SlugExistsAsync(normalizedSlug, excludingId: post.Id, cancellationToken))
        {
            return Conflict(new { message = "Slug already exists." });
        }

        post.Title = form.Title.Trim();
        post.Slug = normalizedSlug;
        post.Excerpt = (form.Excerpt ?? string.Empty).Trim();
        post.Content = (form.Content ?? string.Empty).Trim();
        post.UpdatedAt = DateTime.UtcNow;

        if (form.Published)
        {
            post.PublishedAt ??= DateTime.UtcNow;
        }
        else
        {
            post.PublishedAt = null;
        }

        if (form.RemoveImage && !string.IsNullOrWhiteSpace(post.ImageUrl))
        {
            await _images.DeleteIfLocalAsync(post.ImageUrl, cancellationToken);
            post.ImageUrl = string.Empty;
        }

        if (form.Image != null)
        {
            await _images.DeleteIfLocalAsync(post.ImageUrl, cancellationToken);
            post.ImageUrl = await _images.SaveAsync(form.Image, cancellationToken);
        }

        await _posts.SaveChangesAsync(cancellationToken);
        return Ok(MapDetail(post));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var post = await _posts.GetByIdAsync(id, cancellationToken);
        if (post == null) return NotFound();

        await _images.DeleteIfLocalAsync(post.ImageUrl, cancellationToken);
        await _posts.DeleteAsync(post, cancellationToken);
        await _posts.SaveChangesAsync(cancellationToken);

        return NoContent();
    }

    private static string NormalizeSlug(string slug) =>
        (slug ?? string.Empty).Trim().ToLowerInvariant();

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
