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

            HallTypes = EnumExtensions.GetValuesWithNames<CinemaHallType>();
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
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
                var cinemaHall = await _cinemaHallService.GetCinemaHallAsync(_cinemaHallId);

                if (cinemaHall != null)
                {
                    Name = cinemaHall.Name;
                    Capacity = cinemaHall.Capacity;
                    RowCount = cinemaHall.RowsCount;

                    SelectedType = HallTypes.FirstOrDefault(t => t.Value == cinemaHall.Type);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to load cinema hall: {ex.Message}", "OK");
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
                var updatedHall = new CinemaHallEditDTO(
                    _cinemaHallId,
                    Name,
                    Capacity,
                    SelectedType.Value,
                    RowCount);

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