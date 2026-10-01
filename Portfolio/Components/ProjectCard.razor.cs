using Microsoft.AspNetCore.Components;
using Portfolio.Models;

namespace Portfolio.Components;

public partial class ProjectCard
{
    [Parameter, EditorRequired]
    public Project Project { get; set; } = null!;

    private string DetailUrl => $"projets/{Project.Slug}";

    private bool HasImage => !string.IsNullOrWhiteSpace(Project.ImagePath);

    private bool ShowStatus => Project.Status != ProjectStatus.Done;

    private string MetaLine
    {
        get
        {
            var parts = new List<string> { Project.Period };

            if (!string.IsNullOrWhiteSpace(Project.Role))
                parts.Add(Project.Role);

            if (Project.TeamSize is > 1)
                parts.Add($"équipe de {Project.TeamSize}");

            return string.Join(" · ", parts);
        }
    }
}