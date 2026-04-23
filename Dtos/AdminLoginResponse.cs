namespace BlogApp_API_.Dtos;

public sealed class AdminLoginResponse
{
    public string Token { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public DateTime ExpiresAtUtc { get; set; }
}

