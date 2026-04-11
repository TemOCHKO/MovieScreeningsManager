using MovieScreeningsManager.UI.ViewModels;

namespace MovieScreeningsManager.UI.View;

public partial class CinemaHallsPage : ContentPage
{

    public CinemaHallsPage(CinemaHallViewModel vm)
	{
		// Rendering responsibility (cant reference elements)
		InitializeComponent();
		BindingContext = vm;
    }

}