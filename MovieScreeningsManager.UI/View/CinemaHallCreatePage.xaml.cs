using MovieScreeningsManager.UI.ViewModels;

namespace MovieScreeningsManager.UI.View;

public partial class CinemaHallCreatePage : ContentPage
{
    public CinemaHallCreatePage(CinemaHallCreateViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}