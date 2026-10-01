using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Portfolio.Extensions;
using Portfolio.Models;

namespace Portfolio.Components;

public partial class ProjectModal
{
    [Parameter, EditorRequired]
    public Project Project { get; set; } = null!;

    [Parameter]
    public EventCallback OnClose { get; set; }

    private ElementReference _dialog;

    private string MetaLine => Project.ToMetaLine();

    private bool ShowStatus => Project.Status != ProjectStatus.Done;

    private bool HasImage => !string.IsNullOrWhiteSpace(Project.ImagePath);

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
            await _dialog.FocusAsync();
    }

    private async Task RequestClose()
    {
        await OnClose.InvokeAsync();
    }

    private async Task HandleKeyDown(KeyboardEventArgs e)
    {
        if (e.Key == "Escape")
            await RequestClose();
    }
}