using System.Text.Json.Serialization;

namespace Portfolio.Models;

[JsonConverter(typeof(JsonStringEnumConverter<ProjectStatus>))]
public enum ProjectStatus
{
    Done,
    InProgress,
    Concept
}