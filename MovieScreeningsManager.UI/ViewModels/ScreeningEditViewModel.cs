using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MovieScreeningsManager.Common;
using MovieScreeningsManager.Common.Enums;
using MovieScreeningsManager.DTOModels.Screenings;
using MovieScreeningsManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieScreeningsManager.UI.ViewModels
{
    public partial class ScreeningEditViewModel : BaseViewModel, IQueryAttributable
    {
        private Guid _screeningId;
        private Guid _cinemaHallId; // Keeping this in case you need it for the update payload
        private readonly IScreeningService _screeningService;

        public EnumWithName<FilmGenre>[] Genres { get; }

        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        private DateTime? _launchTime;

        [ObservableProperty]
        private TimeSpan? _startTime;

        [ObservableProperty]
        private EnumWithName<FilmGenre> _selectedGenre;

        [ObservableProperty]
        private int _duration;

        [ObservableProperty]
        private int _yearOfRelease;

        public ScreeningEditViewModel(IScreeningService screeningService)
        {
            _screeningService = screeningService;
            Genres = EnumExtensions.GetValuesWithNames<FilmGenre>();
        }

        // 1. Grab the ID from the Shell navigation parameters
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("Id"))
            {
                _screeningId = (Guid)query["Id"];
            }
        }

        // 2. Fetch the data asynchronously (Called from the Page's OnAppearing)
        public async Task LoadScreeningAsync()
        {
            if (_screeningId == Guid.Empty) return;

            IsBusy = true;
            try
            {
                // Fetch the existing record from your database/service
                var screening = await _screeningService.GetScreeningForEditAsync(_screeningId);

                if (screening != null)
                {
                    _cinemaHallId = screening.CinemaHallId;
                    Title = screening.Name;
                    Duration = screening.Duration;
                    YearOfRelease = screening.YearOfRelease;

                    // Re-split the datetime into Date and Time for the UI pickers
                    LaunchTime = screening.LaunchTime.Date;
                    StartTime = screening.LaunchTime.TimeOfDay;

                    // Match the enum to set the picker selection
                    SelectedGenre = Genres.FirstOrDefault(g => g.Value == screening.FilmGenre);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to load screening: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        // 3. Save the edits back to the database
        [RelayCommand]
        public async Task UpdateScreening()
        {
            IsBusy = true;
            try
            {
                var launch = LaunchTime.HasValue && StartTime.HasValue
                    ? LaunchTime.Value.Date + StartTime.Value
                    : DateTime.Now;

                // Assuming you have an Update DTO. Adjust the class name as needed.
                var updatedScreening = new ScreeningEditDTO(
                    _screeningId,
                    _cinemaHallId,
                    Title,
                    launch,
                    Duration,
                    SelectedGenre.Value,
                    YearOfRelease);

                await _screeningService.UpdateScreeningAsync(updatedScreening);

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to update screening: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task Back()
        {
            IsBusy = true;
            await Shell.Current.GoToAsync("..");
            IsBusy = false;
        }
    }
}