using Portfolio.Models;
namespace Portfolio.Extensions;

public static class ProjectStatusExtensions
{
    public static string ToLabel(this ProjectStatus status) => status switch
    {
        ProjectStatus.Done => "Terminé",
        ProjectStatus.InProgress => "En cours",
        ProjectStatus.Concept => "En conception",
        _ => status.ToString()
    };  
}