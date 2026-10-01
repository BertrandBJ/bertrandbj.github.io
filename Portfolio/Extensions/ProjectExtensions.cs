using Portfolio.Models;

namespace Portfolio.Extensions;

public static class ProjectExtensions
{
    public static string ToMetaLine(this Project project)
    {
        var parts = new List<string> { project.Period };

        if (!string.IsNullOrWhiteSpace(project.Role))
            parts.Add(project.Role);

        if (project.TeamSize is > 1)
            parts.Add($"équipe de {project.TeamSize}");

        return string.Join(" - ", parts);
    }
}