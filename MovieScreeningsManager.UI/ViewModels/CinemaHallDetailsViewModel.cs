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

        [RelayCommand]
        public async Task RefreshData()
        {
            IsBusy = true;
            try
            {
                CurrentCinemaHall = await _cinemaHallService.GetCinemaHallAsync(_cinemaHallId) ?? throw new Exception("Cinema hall not found");
                Screenings = new ObservableCollection<ScreeningListDTO>(await _screeningsTask);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to load cinema hall details: {ex.Message}", "OK");

            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task LoadScreening(Guid screeningId)
        {
            IsBusy = true;
            try
            {
                await Shell.Current.GoToAsync($"{nameof(ScreeningDetailsPage)}", new Dictionary<string, object> { { "ScreeningId", screeningId } });
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to navigate to screening details: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task AddScreening()
        {
            IsBusy = true;
            try
            {
                await Shell.Current.GoToAsync($"{nameof(MovieScreeningCreatePage)}", new Dictionary<string, object> { { nameof(ScreeningCreateDTO.CinemaHallId), _cinemaHallId } });
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to navigate to screening create page: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task DeleteScreening(ScreeningListDTO screening)
        {
            IsBusy = true;
            try
            {
                if (await Shell.Current.DisplayAlertAsync("Confirm", "Are you sure you want to delete this screening?", "Yes", "No"))
                {
                    await _screeningService.DeleteScreeningAsync(screening.Id);
                    Screenings.Remove(screening);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to navigate to screening details: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
