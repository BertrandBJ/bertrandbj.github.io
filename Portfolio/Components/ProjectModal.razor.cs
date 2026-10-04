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

    private string StatusCssClass => Project.Status.ToCssClass();
    
    private IReadOnlyList<ProjectImage> _galleryImages = [];
    
    private int _currentImageIndex = 0;
    private bool HasMultipleImages => _galleryImages.Count > 1;
    private ProjectImage? CurrentImage
    {
        get
        {
            if (_galleryImages.Count == 0)
            {
                return null;
            }

            return _galleryImages[_currentImageIndex];
        }
    }

    protected override void OnParametersSet()
    {
        // Toutes les images sauf la première, qui sert de couverture à la carte
        _galleryImages = Project.Images.Skip(1).ToList();
        _currentImageIndex = 0;
    }

    private void ShowPreviousImage()
    {
        if (_currentImageIndex == 0)
        {
            _currentImageIndex = _galleryImages.Count - 1;
        }
        else
        {
            _currentImageIndex--;
        }
    }

    private void ShowNextImage()
    {
        if (_currentImageIndex == _galleryImages.Count - 1)
        {
            _currentImageIndex = 0;
        }
        else
        {
            _currentImageIndex++;
        }
    }

    private void ShowImage(int index)
    {
        _currentImageIndex = index;
    }

    private string DotCssClass(int index)
    {
        if (index == _currentImageIndex)
        {
            return "gallery-dot gallery-dot--active";
        }

        return "gallery-dot";
    }
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
        {
            await RequestClose();
        }
        else if (e.Key == "ArrowLeft" && HasMultipleImages)
        {
            ShowPreviousImage();
        }
        else if (e.Key == "ArrowRight" && HasMultipleImages)
        {
            ShowNextImage();
        }
    }
}