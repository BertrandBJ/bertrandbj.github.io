namespace Portfolio.Models;

public record Project
{
    public required string Slug { get; init; }
    public required string Title { get; init; }
    public required string Period { get; init; }
    public required ProjectStatus Status { get; init; }
    public string? Role { get; init; }
    public int? TeamSize { get; init; }
    public required string ShortDescription { get; init; }
    
    public IReadOnlyList<ProjectSection> Sections { get; init; } = [];
    public IReadOnlyList<string> Technologies { get; init; } = [];
    
    public IReadOnlyList<ProjectImage> Images { get; init; } = [];
    public string? RepoUrl { get; init; }
    public string? LiveUrl { get; init; }
    public int Order { get; init; }
    
}