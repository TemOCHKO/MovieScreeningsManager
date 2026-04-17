using MovieScreeningsManager.UI.ViewModels;

namespace MovieScreeningsManager.UI.View;

public partial class CinemaHallsPage : ContentPage
{
    private CinemaHallViewModel _viewModel;

    public CinemaHallsPage(CinemaHallViewModel vm)
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