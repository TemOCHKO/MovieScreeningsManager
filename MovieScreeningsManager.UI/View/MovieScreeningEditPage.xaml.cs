namespace MovieScreeningsManager.UI.View;

using global::MovieScreeningsManager.UI.ViewModels;

public partial class MovieScreeningEditPage : ContentPage
{
    private readonly ScreeningEditViewModel _viewModel;

    public MovieScreeningEditPage(ScreeningEditViewModel vm)
    {
        InitializeComponent();
        _viewModel = vm;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // Trigger the database fetch safely after the UI is ready
        await _viewModel.LoadScreeningAsync();
    }
}