using MovieScreeningsManager.Services;
using MovieScreeningsManager.UI.ViewModels;
namespace MovieScreeningsManager.UI.View;

public partial class ScreeningDetailsPage : ContentPage
{

    private ScreeningDetailsViewModel _viewModel;

    public ScreeningDetailsPage(ScreeningDetailsViewModel vm)
    {
        // Rendering responsibility (cant reference elements)
        InitializeComponent();
        _viewModel = vm;
        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        await _viewModel.RefreshData();
    }
}