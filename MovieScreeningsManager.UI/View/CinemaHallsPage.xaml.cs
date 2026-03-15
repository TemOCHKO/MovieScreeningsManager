using MovieScreeningsManager.Services;
using MovieScreeningsManager.UIModels;
using System.Collections.ObjectModel;

namespace MovieScreeningsManager.UI.View;

public partial class CinemaHallsPage : ContentPage
{
	private StorageService _storage;
    public ObservableCollection<CinemaHallUIModel> CinemaHalls { get; set; } = new ObservableCollection<CinemaHallUIModel>();
    public CinemaHallsPage()
	{
		// Rendering responsibility (cant reference elements)
		InitializeComponent();
		_storage = new StorageService();
		foreach (var hall in _storage.GetAllCinemaHalls())
		{
			CinemaHalls.Add(new CinemaHallUIModel(hall));
            //CinemaHalls.Add(new CinemaHallUIModel(hall.Id, hall.Name, hall.Capacity, hall.Type));
        }
		BindingContext = this;
    }

    private void CinemaHallSelected(object sender, SelectionChangedEventArgs e)
    {
		var cinema = (CinemaHallUIModel)e.CurrentSelection[0];
		Shell.Current.GoToAsync($"CinemaHallDetailsPage", new Dictionary<string, object> { { "SelectedCinemaHall", cinema} });
    }
}