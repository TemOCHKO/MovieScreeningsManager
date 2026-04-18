using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MovieScreeningsManager.DTOModels.CinemaHalls;
using MovieScreeningsManager.DTOModels.Screenings;
using MovieScreeningsManager.Services;
using MovieScreeningsManager.UI.View;
using System.Collections.ObjectModel;


namespace MovieScreeningsManager.UI.ViewModels
{ 
    public partial class CinemaHallDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly ICinemaHallService _cinemaHallService;
        private readonly IScreeningService _screeningService;

        private Task<CinemaHallDetailsDTO> _cinemaHallTask;
        private Task<IEnumerable<ScreeningListDTO>> _screeningsTask;

        private Guid _cinemaHallId;

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
            _cinemaHallId = (Guid)query["CinemaHallId"];
            _cinemaHallTask = _cinemaHallService.GetCinemaHallAsync(_cinemaHallId);
            _screeningsTask = _screeningService.GetScreeningsByCinemaHallAsync(_cinemaHallId);
            OnPropertyChanged(nameof(Screenings));
        }

        internal async Task RefreshData()
        {
            IsBusy = true;
            CurrentCinemaHall = await _cinemaHallTask;
            Screenings = new ObservableCollection<ScreeningListDTO>(await _screeningsTask);
            IsBusy = false;
        }

        [RelayCommand]
        private async Task LoadScreening(Guid screeningId)
        {
            IsBusy = true;
            await Shell.Current.GoToAsync($"{nameof(ScreeningDetailsPage)}", new Dictionary<string, object> { { "ScreeningId", screeningId } });
            IsBusy = false;
        }
    }
}
