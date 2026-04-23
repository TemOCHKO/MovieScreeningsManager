using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MovieScreeningsManager.Common;
using MovieScreeningsManager.Common.Enums;
using MovieScreeningsManager.DTOModels.CinemaHalls;
using MovieScreeningsManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieScreeningsManager.UI.ViewModels
{
    public partial class CinemaHallEditViewModel : BaseViewModel, IQueryAttributable
    {
        private Guid _cinemaHallId;
        private readonly ICinemaHallService _cinemaHallService;

        // Assuming you have an enum called CinemaHallType
        public EnumWithName<CinemaHallType>[] HallTypes { get; }

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private int _capacity;

        [ObservableProperty]
        private int _rowCount;

        [ObservableProperty]
        private EnumWithName<CinemaHallType> _selectedType;

        public CinemaHallEditViewModel(ICinemaHallService cinemaHallService)
        {
            _cinemaHallService = cinemaHallService;

            // Populate the picker options using your existing extension method
            HallTypes = EnumExtensions.GetValuesWithNames<CinemaHallType>();
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            // Make sure the key matches what you send from the list page!
            if (query.ContainsKey("CinemaHallId"))
            {
                _cinemaHallId = (Guid)query["CinemaHallId"];
            }
        }

        public async Task LoadCinemaHallAsync()
        {
            if (_cinemaHallId == Guid.Empty) return;

            IsBusy = true;
            try
            {
                // Assuming GetCinemaHallAsync returns your details DTO
                var cinemaHall = await _cinemaHallService.GetCinemaHallAsync(_cinemaHallId);

                if (cinemaHall != null)
                {
                    Name = cinemaHall.Name;
                    Capacity = cinemaHall.Capacity;
                    RowCount = cinemaHall.RowsCount;

                    // Match the enum to set the picker selection
                    SelectedType = HallTypes.FirstOrDefault(t => t.Value == cinemaHall.Type);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to load cinema hall: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task UpdateCinemaHall()
        {
            IsBusy = true;
            try
            {
                // Create your update DTO. Adjust the class name if yours is different.
                var updatedHall = new CinemaHallEditDTO(
                    _cinemaHallId,
                    Name,
                    Capacity,
                    SelectedType.Value,
                    RowCount);

                // Call your SQLite service to update
                await _cinemaHallService.UpdateCinemaHallAsync(updatedHall);

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to update cinema hall: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task Back()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}