using MovieScreeningsManager.UI.ViewModels;

namespace MovieScreeningsManager.UI.View;
public partial class CinemaHallDetailsPage : ContentPage
{
	
	public CinemaHallDetailsPage(CinemaHallDetailsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }
}