using MovieScreeningsManager.Services;
using MovieScreeningsManager.UIModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace MovieScreeningsManager.UI.ViewModel
{
    public partial class CinemaHallViewModel : BaseViewModel
    {
        StorageService _storageService;
        public ObservableCollection<CinemaHallUIModel> CinemaHalls { get; } = new();

        public Command GetCinemasCommand { get; }

        public CinemaHallViewModel(StorageService storage)
        { 
            Title = "Cinema Finder";
            _storageService = storage;
            GetCinemasCommand = new Command(GetCinamas);
        }

        public void GetCinamas()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                var cinemaHalls = _storageService.GetAllCinemaHalls();

                if (CinemaHalls.Count != 0)
                    CinemaHalls.Clear();

                foreach (var cinemaHall in cinemaHalls)
                {
                    CinemaHalls.Add(new CinemaHallUIModel(cinemaHall));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching cinema halls: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
