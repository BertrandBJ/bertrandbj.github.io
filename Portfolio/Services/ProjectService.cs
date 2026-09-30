using System.Net.Http.Json;
using Portfolio.Models;

namespace Portfolio.Services;

public class ProjectService : IProjectService
{
    private const string DataPath = "data/projects.json";

    private readonly HttpClient _http;
    private IReadOnlyList<Project>? _projects;

    public ProjectService(HttpClient http)
    {
        _http = http;
    }

    public async Task<IReadOnlyList<Project>> GetAllAsync()
    {
        if (_projects is not null)
            return _projects;

        var projects = await _http.GetFromJsonAsync<List<Project>>(DataPath) ?? [];
        _projects = projects.OrderBy(p => p.Order).ToList();
        return _projects;
    }

    public async Task<Project?> GetBySlugAsync(string slug)
    {
        var projects = await GetAllAsync();
        return projects.FirstOrDefault(p =>
            string.Equals(p.Slug, slug, StringComparison.OrdinalIgnoreCase));
    }
}