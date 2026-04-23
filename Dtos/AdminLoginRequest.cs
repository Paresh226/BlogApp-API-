using System.ComponentModel.DataAnnotations;

namespace BlogApp_API_.Dtos;

public sealed class AdminLoginRequest
{
    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}

