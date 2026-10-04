using Microsoft.AspNetCore.Components;
using Portfolio.Models;
using Portfolio.Extensions;
namespace Portfolio.Components;

public partial class ProjectCard
{
    [Parameter, EditorRequired] // project est un parameter qui doit être passé par la page home de blazor (editorRequired => refuse si parameter null).
    public Project Project { get; set; } = null!; // typage Project (voir model projet).

    private string DetailUrl => $"projets/{Project.Slug}";

    private ProjectImage? CoverImage
    {
        get
        {
            if (Project.Images.Count > 0)
            {
                return Project.Images[0];
            }

            return null;
        }
    }

    private string StatusCssClass => Project.Status.ToCssClass();
    private bool ShowStatus => Project.Status != ProjectStatus.Done;

    private string MetaLine => Project.ToMetaLine(); // from l'extension.
    
}