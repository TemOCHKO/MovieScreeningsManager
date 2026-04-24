using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MovieScreeningsManager.Common;
using MovieScreeningsManager.Common.Enums;
using MovieScreeningsManager.DTOModels.CinemaHalls;
using MovieScreeningsManager.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MovieScreeningsManager.UI.ViewModels
{
    public partial class CinemaHallCreateViewModel : BaseViewModel
    {
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

        public CinemaHallCreateViewModel(ICinemaHallService cinemaHallService)
        {
            _cinemaHallService = cinemaHallService;
            HallTypes = EnumExtensions.GetValuesWithNames<CinemaHallType>();
            SelectedType = HallTypes.FirstOrDefault(); 
        }

        [RelayCommand]
        public async Task SaveCinemaHall()
        {
          
            IsBusy = true;
            try
            {
                var newHall = new CinemaHallCreateDTO(
                    Name,
                    Capacity,
                    SelectedType.Value,
                    RowCount);

                await _cinemaHallService.CreateCinemaHallAsync(newHall);

                // Go back to the list
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to create cinema hall: {ex.Message}", "OK");
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