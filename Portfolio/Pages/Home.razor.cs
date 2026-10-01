using Microsoft.AspNetCore.Components;
using Portfolio.Models;
using Portfolio.Services;

namespace Portfolio.Pages;

public partial class Home
{
    [Inject]
    private IProjectService ProjectService { get; set; } =null!; //propriété, blazor need get/set pour inject.

    private IReadOnlyList<Project>? _projects; //champ
    
    private static readonly IReadOnlyList<string> HeroTechnologies =
    [
        "C#", ".NET", "Blazor", "ASP.NET Core", "TypeScript", "Next.js", "NestJS"
    ];

    private static readonly IReadOnlyList<SkillGroup> SkillGroups =
    [
        new("Langages", ["C#", "TypeScript", "JavaScript", "PHP", "SQL", "HTML/CSS"]),
        new("Back-end", [".NET 8", "ASP.NET Core", "NestJS", "Node.js", "API REST", "Architecture MVC"]),
        new("Front-end", ["Blazor", "Next.js (React)", ".NET MAUI (en cours)"]),
        new("Données", ["SQL Server", "PostgreSQL", "SQLite", "Entity Framework Core", "Prisma", "MongoDB (en cours)"]),
        new("Outils et méthodes", ["Git", "Docker", "Tests unitaires", "CI/CD", "Scrum", "Jira", "Confluence"]),
        new("Langues", ["Anglais C2"])
    ];

    // je met en dur ici pour le moment, dunno si je vais le mettre dans un JSON plustard. ou si je le fais quand je décide d'ajouter un backend plus sérieux.
    
    protected override async Task OnInitializedAsync()
    {
        _projects = await ProjectService.GetAllAsync();
    }
}