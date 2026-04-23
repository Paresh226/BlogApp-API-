namespace BlogApp_API_.Options;

public sealed class JwtOptions
{
    public string Secret { get; set; } = string.Empty;
    public int ExpiryMinutes { get; set; } = 60 * 12;
}

