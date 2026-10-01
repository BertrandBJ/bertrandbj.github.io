using Microsoft.AspNetCore.Components;
using Portfolio.Models;

namespace Portfolio.Components;

public partial class ProjectCard
{
    [Parameter, EditorRequired] // project est un parameter qui doit être passé par la page home de blazor (editorRequired => refuse si parameter null).
    public Project Project { get; set; } = null!; // typage Project (voir model projet).

    private string DetailUrl => $"projets/{Project.Slug}";

    private bool HasImage => !string.IsNullOrWhiteSpace(Project.ImagePath);

    private bool ShowStatus => Project.Status != ProjectStatus.Done;

    private string MetaLine // data en plus comme : period/role/teamsize.
    {
        get
        {
            var parts = new List<string> { Project.Period };

            if (!string.IsNullOrWhiteSpace(Project.Role))
                parts.Add(Project.Role); // check si project.role est null/vide/whitespace, si ce n'est pas le cas => add à parts.

            if (Project.TeamSize is > 1)
                parts.Add($"équipe de {Project.TeamSize}"); 

            return string.Join(" - ", parts);
        }
    }
}