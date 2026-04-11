using MovieScreeningsManager.DTOModels.CinemaHalls;
using MovieScreeningsManager.Services;
using MovieScreeningsManager.UI.View;
using System.Collections.ObjectModel;

namespace MovieScreeningsManager.UI.ViewModels
{
    public class CinemaHallViewModel
    {
        private readonly ICinemaHallService _cinemaHallService;
        public ObservableCollection<CinemaHallListDTO> CinemaHalls { get; set; }
        public CinemaHallListDTO SelectedCinemaHall { get; set; }
        public Command CinemaHallSelectedCommand { get; }
        public CinemaHallViewModel(ICinemaHallService cinemaHallService)
        {
            // Rendering responsibility (cant reference elements)

            _cinemaHallService = cinemaHallService;

            CinemaHalls = new ObservableCollection<CinemaHallListDTO>(_cinemaHallService.GetCinemaHalls());
            CinemaHallSelectedCommand = new Command(LoadCinemaHall);

        }

        private void LoadCinemaHall()
        {
            if (SelectedCinemaHall == null)
                return;

            Shell.Current.GoToAsync($"{nameof(CinemaHallDetailsPage)}", new Dictionary<string, object> { { "CinemaHallId", SelectedCinemaHall.Id } });
        }
    }
}
