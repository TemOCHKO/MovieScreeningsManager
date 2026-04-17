using MovieScreeningsManager.UI.ViewModels;

namespace MovieScreeningsManager.UI.View;
public partial class CinemaHallDetailsPage : ContentPage
{

    private CinemaHallDetailsViewModel _viewModel;

    public CinemaHallDetailsPage(CinemaHallDetailsViewModel vm)
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