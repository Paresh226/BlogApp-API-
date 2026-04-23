namespace BlogApp_API_.Options;

public sealed class UiCorsOptions
{
    public string[] AllowedOrigins { get; set; } = new[] { "http://localhost:5173" };
}

