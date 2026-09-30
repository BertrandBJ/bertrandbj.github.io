using Microsoft.AspNetCore.Components;
using Portfolio.Models;
using Portfolio.Services;

namespace Portfolio.Pages;

public partial class Home
{
    [Inject]
    private IProjectService ProjectService { get; set; } =null!;

    private IReadOnlyList<Project>? _projects;

    protected override async Task OnInitializedAsync()
    {
        _projects = await ProjectService.GetAllAsync();
    }
}