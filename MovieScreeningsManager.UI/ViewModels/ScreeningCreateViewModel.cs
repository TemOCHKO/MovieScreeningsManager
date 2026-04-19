using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MovieScreeningsManager.Common;
using MovieScreeningsManager.Common.Enums;
using MovieScreeningsManager.DTOModels.Screenings;
using MovieScreeningsManager.Services;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MovieScreeningsManager.UI.ViewModels
{
    public partial class ScreeningCreateViewModel : BaseViewModel, IQueryAttributable
    {
        private Guid _cinemaHallId;
        private EnumWithName<FilmGenre>[] _genres;

        public EnumWithName<FilmGenre>[] Genres => _genres;

        private readonly IScreeningService _screeningService;

        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        private DateTime? _launchTime = DateTime.Today;

        [ObservableProperty]
        private TimeSpan? _startTime = DateTime.Now.TimeOfDay;

        [ObservableProperty]
        private EnumWithName<FilmGenre> _selectedGenre;

        [ObservableProperty]
        private int _duration;

        [ObservableProperty]
        private int _yearOfRelease;


        [ObservableProperty]
        private Dictionary<string, string> _errors;


        public ScreeningCreateViewModel(IScreeningService screeningService)
        {
            _screeningService = screeningService;
            _genres = EnumExtensions.GetValuesWithNames<FilmGenre>();
            _selectedGenre = _genres.FirstOrDefault();
            Errors = InitErrors();
        }
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            _cinemaHallId = (Guid)query[nameof(ScreeningCreateDTO.CinemaHallId)];
        }

        [RelayCommand]
        public async Task CreateScreening()
        {
            IsBusy = true;

           /* var errors = Validator.ValidateScreening(Title, SelectedGenre.Value, LaunchTime, Duration, YearOfRelease);
            Errors = InitErrors();
            if (errors.Count > 0)
            {
                foreach (var error in errors)
                {
                    if (String.IsNullOrWhiteSpace(Errors[error.memberName]))
                    {
                        Errors[error.memberName] = error.errorMessage;
                        continue;
                    }
                    Errors[error.memberName] += Environment.NewLine + error.errorMessage;
                }
                OnPropertyChanged(nameof(Errors));
                IsBusy = false;
                return;
            }*/
            try
            {
                var launch = LaunchTime.HasValue && StartTime.HasValue ? LaunchTime.Value.Date + StartTime.Value : DateTime.Now;
                var newScreening = new ScreeningCreateDTO(_cinemaHallId, Title, launch, Duration, SelectedGenre.Value, YearOfRelease);
                await _screeningService.CreateScreeningAsync(newScreening);
                //await Shell.Current.DisplayAlertAsync("Success", "Screening created successfully!", "OK");
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to create screening: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task Back()
        {
            try
            {
                IsBusy = true;
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to navigate back: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private Dictionary<string, string> InitErrors()
        {
            return new Dictionary<string, string>()
            {
                { nameof(Title), string.Empty },
                { nameof(LaunchTime), string.Empty },
                { nameof(StartTime), string.Empty  },
                { nameof(SelectedGenre), string.Empty },
                { nameof(Duration), string.Empty },
                { nameof(YearOfRelease), string.Empty }
            };

        }
    }
}
