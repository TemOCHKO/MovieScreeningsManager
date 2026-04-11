using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MovieScreeningsManager.DTOModels.CinemaHalls;
using MovieScreeningsManager.DTOModels.Screenings;
using MovieScreeningsManager.Services;
using MovieScreeningsManager.UI.View;
using System.Collections.ObjectModel;


namespace MovieScreeningsManager.UI.ViewModels
{ 
    public partial class CinemaHallDetailsViewModel : ObservableObject, IQueryAttributable
    {
        private readonly ICinemaHallService _cinemaHallService;
        private readonly IScreeningService _screeningService;
        [ObservableProperty]
        private CinemaHallDetailsDTO _currentCinemaHall;
        [ObservableProperty]
        private ObservableCollection<ScreeningListDTO> _screenings;

        public CinemaHallDetailsViewModel(ICinemaHallService cinemaHallService, IScreeningService screeningService)
        {
            _cinemaHallService = cinemaHallService;
            _screeningService = screeningService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var cinemaHallId = (Guid)query["CinemaHallId"];
            CurrentCinemaHall = _cinemaHallService.GetCinemaHall(cinemaHallId);
            Screenings = new ObservableCollection<ScreeningListDTO>(_screeningService.GetScreeningsByCinemaHall(cinemaHallId));
            OnPropertyChanged(nameof(Screenings));
        }

        [RelayCommand]
        private void LoadScreening(Guid screeningId)
        {
            Shell.Current.GoToAsync($"{nameof(ScreeningDetailsPage)}", new Dictionary<string, object> { { "ScreeningId", screeningId } });
        }
    }
}
