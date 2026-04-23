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

        [RelayCommand]
        private async Task EditCinemaHall(CinemaHallListDTO cinemaHall)
        {
            if (cinemaHall == null)
                return;

            // Example navigation to an edit page. Adjust the route to match your app.

            await Shell.Current.GoToAsync($"{nameof(CinemaHallEditPage)}", new Dictionary<string, object> { { "CinemaHallId", cinemaHall.Id } });
        }

        [RelayCommand]
        private async Task DeleteCinemaHall(CinemaHallListDTO cinemaHall)
        {
            if (cinemaHall == null)
                return;

            bool confirm = await Shell.Current.DisplayAlertAsync("Confirm Delete", $"Are you sure you want to delete {cinemaHall.Name}?", "Yes", "No");

            if (confirm)
            {
                IsBusy = true;
                try
                {
                    // Call your service to delete it from the database
                    await _cinemaHallService.DeleteCinemaHallAsync(cinemaHall.Id);

                    // Remove it from the UI list so it disappears immediately
                    CinemaHalls.Remove(cinemaHall);
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlertAsync("Error", $"Failed to delete: {ex.Message}", "OK");
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }
    }
}
