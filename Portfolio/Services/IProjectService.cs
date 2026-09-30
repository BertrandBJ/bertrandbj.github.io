using Portfolio.Models;
namespace Portfolio.Services;

public interface IProjectService
{
    Task<IReadOnlyList<Project>> GetAllAsync();
    Task<Project?> GetBySlugAsync(string slug);
}