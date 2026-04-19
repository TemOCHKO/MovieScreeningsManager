using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MovieScreeningsManager.DTOModels.CinemaHalls;
using MovieScreeningsManager.Services;
using MovieScreeningsManager.UI.View;
using System.Collections.ObjectModel;

namespace MovieScreeningsManager.UI.ViewModels
{
    public partial class CinemaHallViewModel : BaseViewModel
    {
        private readonly ICinemaHallService _cinemaHallService;
        [ObservableProperty]
        public ObservableCollection<CinemaHallListDTO> _cinemaHalls;
        [ObservableProperty]
        public CinemaHallListDTO _selectedCinemaHall;
        public Command CinemaHallSelectedCommand { get; }
        public CinemaHallViewModel(ICinemaHallService cinemaHallService)
        {
            // Rendering responsibility (cant reference elements)

            _cinemaHallService = cinemaHallService;
        }

        internal async Task RefreshData()
        {
            IsBusy = true;
            try
            {
                CinemaHalls = new ObservableCollection<CinemaHallListDTO>();
                await foreach (var cinemaHall in _cinemaHallService.GetCinemaHallsAsync())
                {
                    CinemaHalls.Add(cinemaHall);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to load screennings: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }

        }

        [RelayCommand]
        private async Task LoadCinemaHall()
        {
            IsBusy = true;
            try
            {
                if (SelectedCinemaHall == null)
                    return;

                await Shell.Current.GoToAsync($"{nameof(CinemaHallDetailsPage)}", new Dictionary<string, object> { { "CinemaHallId", SelectedCinemaHall.Id } });

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to navigate: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
