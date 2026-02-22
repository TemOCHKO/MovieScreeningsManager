using MovieScreeningsManager.Common.Enums;
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


        // Add methods
        public bool AddCinemaHall(CinemaHallDBModel cinemaHall)
        {
            if (_cinemaHalls.Any(ch => ch.Id == cinemaHall.Id))
                return false;

            _cinemaHalls.Add(cinemaHall);
            return true;
        }

        public bool AddScreening(ScreeningDBModel screening)
        {
            if (_screenings.Any(s => s.Id == screening.Id))
                return false;

            if (screening != null)
            {
                _screenings.Add(screening);
                return true;
            }
            return false;
        }


        // Update methods
        public bool UpdateCinemaHall(CinemaHallDBModel cinemaHall)
        {
            var index = _cinemaHalls.FindIndex(ch => ch.Id == cinemaHall.Id);
            if (index == -1)
                return false;
            _cinemaHalls[index] = cinemaHall;
            return true;
        }

        public bool UpdateScreening(ScreeningDBModel screening)
        {
            var index = _screenings.FindIndex(s => s.Id == screening.Id);
            if (index == -1)
                return false;
            _screenings[index] = screening;
            return true;
        }

        // Remove methods
        public bool RemoveCinemaHall(CinemaHallDBModel cinemaHall)
        {
            if (_cinemaHalls.Remove(cinemaHall))
            {
                return true;
            }
            return false;
        }

        public bool RemoveScreening(ScreeningDBModel screening)
        {
            if (_screenings.Remove(screening))
            {
                return true;
            }
            return false;

        }

        // Filter and sort methods
        public IEnumerable<ScreeningDBModel> FilterScreeningsByGenre(FilmGenre filmGenre)
        {
            return _screenings.Where(s => s.Genre == filmGenre).ToList();
        }

        public IEnumerable<ScreeningDBModel> FilterScreeningsByYearOfRelease(int yearOfRelease)
        {
            return _screenings.Where(s => s.YearOfRelease == yearOfRelease).ToList();
        }

        public IEnumerable<ScreeningDBModel> SortByName(bool reverse = false)
        {
            if (reverse)
            {
                return _screenings.OrderByDescending(s => s.Name).ToList();
            }
            return _screenings.OrderBy(s => s.Name).ToList();
        }

        public IEnumerable<ScreeningDBModel> SortByLaunchTime(bool reverse = false)
        {
            if (reverse)
            {
                return _screenings.OrderByDescending(s => s.LaunchTime).ToList();
            }
            return _screenings.OrderBy(s => s.LaunchTime).ToList();

        }
    }
}
