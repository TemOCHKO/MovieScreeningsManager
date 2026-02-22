using MovieScreeningsManager.DBModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieScreeningsManager.Services
{
    public class StorageService
    {
        private List<CinemaHallDBModel> _cinemaHalls;
        private List<ScreeningDBModel> _screenings;

        private void LoadData()
        {
            if (_cinemaHalls != null && _screenings != null) return;
            _cinemaHalls = FakeStorage.CinemaHalls.ToList();
            _screenings = FakeStorage.Screenings.ToList();
        }

        public IEnumerable<ScreeningDBModel> GetScreenings(Guid cinemaHallId)
        {
            LoadData();
            var resultingList = new List<ScreeningDBModel>();
            foreach (var screening in _screenings)
            {
                if (screening.CinemaHallId == cinemaHallId) 
                { 
                    resultingList.Add(screening);
                }
            }
            return resultingList;
        }

        public IEnumerable<CinemaHallDBModel> GetAllCinemaHalls()
        {
            LoadData();
            // if so, we return the actual list 
            //return _cinemaHalls;

            var resultingList = new List<CinemaHallDBModel>();
            foreach (var cinemaHall in _cinemaHalls)
            {
                resultingList.Add(cinemaHall);
            }
            return resultingList;
        }
    }
}
