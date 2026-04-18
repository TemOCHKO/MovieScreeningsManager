using CommunityToolkit.Mvvm.ComponentModel;
using MovieScreeningsManager.Common.Enums;
using MovieScreeningsManager.DTOModels.Screenings;
using MovieScreeningsManager.Services;

namespace MovieScreeningsManager.UI.ViewModels
{
    public partial class ScreeningDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly IScreeningService _screeningService;
        private ScreeningDetailsDTO _currentScreening;

        private DateTime _endTime;
        public String Name => _currentScreening?.Name;
        public DateTime? LaunchTime => _currentScreening?.LaunchTime;
        public int Duration => _currentScreening?.Duration ?? 0;
        public FilmGenre Genre => _currentScreening?.FilmGenre ?? FilmGenre.Action;
        public int YearOfRelease => _currentScreening?.YearOfRelease ?? 0;
        public DateTime EndTime => _endTime;

        private Guid _screeningId;

        private void CalculateEndTime()
        {
            if (_currentScreening != null)
            {
                _endTime = _currentScreening.LaunchTime.AddMinutes(_currentScreening.Duration);
            }
        }
        public ScreeningDetailsViewModel(IScreeningService screeningService)
        {
            _screeningService = screeningService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            _screeningId = (Guid)query["ScreeningId"];
        }

        internal async Task RefreshData()
        {
            IsBusy = true;
            _currentScreening = await _screeningService.GetScreeningAsync(_screeningId);
            CalculateEndTime();
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(LaunchTime));
            OnPropertyChanged(nameof(Duration));
            OnPropertyChanged(nameof(Genre));
            OnPropertyChanged(nameof(YearOfRelease));
            OnPropertyChanged(nameof(EndTime));
            IsBusy = false;
        }
    }
}
