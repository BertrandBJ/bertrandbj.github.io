namespace Portfolio.Models;

public record ProjectSection
{
    public required string Title { get; init; }
    public string? Text { get; init; }
    public IReadOnlyList<string> Items { get; init; } = [];
}