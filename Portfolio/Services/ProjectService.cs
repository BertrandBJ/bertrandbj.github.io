using System.Net.Http.Json;
using Portfolio.Models;


// service qui va choper les data dans le project.json 
namespace Portfolio.Services;

public class ProjectService : IProjectService
{
    private const string DataPath = "data/projects.json";

    private readonly HttpClient _http;
    
    private IReadOnlyList<Project>? _projects; // Cache mémoire où est le projet, en readOnly (pour pas être modifiable sur les pages) ? => nullable.  Filled quand GetAllAsync() est called dans home.

    public ProjectService(HttpClient http)
    {
        _http = http; // constructeur avec l'injection from program.cs
    }

    public async Task<IReadOnlyList<Project>> GetAllAsync()
    {
        if (_projects is not null) // si le cache est déjà full, no need re_dl data.
            return _projects;

        var projects = await _http.GetFromJsonAsync<List<Project>>(DataPath) ?? []; //[] => new List<Project>() vide.
        _projects = projects.OrderBy(p => p.Order).ToList(); // Order ici est une data dans le datajson. 
        return _projects;
    }

    public async Task<Project?> GetBySlugAsync(string slug)
    {
        var projects = await GetAllAsync(); // get data si le cache est vide.
        return projects.FirstOrDefault(p =>
            string.Equals(p.Slug, slug, StringComparison.OrdinalIgnoreCase)); // search par clé  slug, used pour pathing url quand on utilisateur va click sur un projet pour les détails.
    }
}