using Microsoft.AspNetCore.Components;

namespace Portfolio.Components;

public partial class TechBadge : ComponentBase
{
    [Parameter, EditorRequired]
    public string Name { get; set; } = null!;
}