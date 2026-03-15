using MovieScreeningsManager.Services;
using MovieScreeningsManager.UIModels;

namespace MovieScreeningsManager.UI.View;

[QueryProperty(nameof(CurrentCinemaHall), "SelectedCinemaHall")]
public partial class CinemaHallDetailsPage : ContentPage
{
	private CinemaHallUIModel _currentCinemaHall;
	public CinemaHallUIModel CurrentCinemaHall 
	{ 
		get => _currentCinemaHall;
		set
		{
			_currentCinemaHall = value;
			var storage = new StorageService();
			_currentCinemaHall.LoadScreenings(storage);
			BindingContext = CurrentCinemaHall;
		}
	}
	public CinemaHallDetailsPage()
	{
		InitializeComponent();
	}

    private void ScreeningSelected(object sender, SelectionChangedEventArgs e)
    {
		var screening = (ScreeningUIModel)e.CurrentSelection[0];
        Shell.Current.GoToAsync($"ScreeningDetailsPage", new Dictionary<string, object> { { "SelectedScreening", screening } });

    }
}