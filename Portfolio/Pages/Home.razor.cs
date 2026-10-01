using Microsoft.AspNetCore.Components;
using Portfolio.Models;
using Portfolio.Services;
using Microsoft.JSInterop;

namespace Portfolio.Pages;

public partial class Home
{
    [Parameter]
    public string? Slug { get; set; }
    
    [Inject]
    private IJSRuntime JS { get; set; } = null!;

    private double? _scrollToRestore;

    [Inject]
    private IProjectService ProjectService { get; set; } = null!;

    [Inject]
    private NavigationManager Navigation { get; set; } = null!;

    private IReadOnlyList<Project>? _projects;
    
    private Project? _selectedProject;

    private string PageTitleText => _selectedProject is null
        ? "Bertrand Beaujard – Développeur full stack C# / .NET"
        : $"{_selectedProject.Title} – Bertrand Beaujard";

    
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

    protected override async Task OnParametersSetAsync()
    {
        _scrollToRestore = await JS.InvokeAsync<double>("portfolioScroll.getY");
        
        if (string.IsNullOrWhiteSpace(Slug))
        {
            _selectedProject = null;
            return;
        }

        _selectedProject = await ProjectService.GetBySlugAsync(Slug);

        if (_selectedProject is null)
            Navigation.NavigateTo("", replace: true);
    }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (_scrollToRestore.HasValue)
        {
            double y = _scrollToRestore.Value;
            _scrollToRestore = null;
            await JS.InvokeVoidAsync("portfolioScroll.restoreY", y);
        }
    }

    private void CloseModal()
    {
        Navigation.NavigateTo("");
    }
}
