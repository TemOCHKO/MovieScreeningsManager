namespace MovieScreeningsManager.UI.View;

using global::MovieScreeningsManager.UI.ViewModels;

public partial class CinemaHallEditPage : ContentPage
{
    private readonly CinemaHallEditViewModel _viewModel;

    public CinemaHallEditPage(CinemaHallEditViewModel vm)
    {
        InitializeComponent();
        _viewModel = vm;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadCinemaHallAsync();
    }
}