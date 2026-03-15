using MovieScreeningsManager.Services;
using MovieScreeningsManager.UIModels;

namespace MovieScreeningsManager.UI.View;

[QueryProperty(nameof(CurrentScreening), "SelectedScreening")]
public partial class ScreeningDetailsPage : ContentPage
{
    private ScreeningUIModel _currentScreening;
    public ScreeningUIModel CurrentScreening
    {
        get => _currentScreening;
        set
        {
            _currentScreening = value;
            BindingContext = CurrentScreening;
        }
    }
    public ScreeningDetailsPage()
    {
        InitializeComponent();
    }
}