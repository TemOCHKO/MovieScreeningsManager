
using MovieScreeningsManager.Common.Enums;
using MovieScreeningsManager.DBModels;


namespace MovieScreeningsManager.Storage 
{
    public class InMemoryStorageContext : IStorageContext
    {
        private record CinemaHallRecord (Guid Id, string Name, int Capacity, CinemaHallType Type, int rowCount);
        private record ScreeningRecord (Guid Id, string name, FilmGenre genre, int yearOfRelease, DateTime launchTime, int duration, Guid cinemaHallId);
        private static readonly List<CinemaHallRecord> _cinemaHalls = new List<CinemaHallRecord>();
        private static readonly List<ScreeningRecord> _screenings = new List<ScreeningRecord>();

        static InMemoryStorageContext()
        {
            #region Initialization of in-memory data    
            _cinemaHalls.Add(new CinemaHallRecord(Guid.NewGuid(), "Cinema Hall 1", 100, CinemaHallType.Standard, 10));
            _cinemaHalls.Add(new CinemaHallRecord(Guid.NewGuid(), "Cinema Hall 2", 150, CinemaHallType.IMAX, 17));
            _cinemaHalls.Add(new CinemaHallRecord(Guid.NewGuid(), "Cinema Hall 3", 200, CinemaHallType.ThreeD, 18));

            _screenings.Add(new ScreeningRecord(Guid.NewGuid(), "Film 1", FilmGenre.Action, 2020, DateTime.Now.AddHours(1), 120, _cinemaHalls[0].Id));
            _screenings.Add(new ScreeningRecord(Guid.NewGuid(), "Film 2", FilmGenre.Comedy, 2021, DateTime.Now.AddHours(2), 90, _cinemaHalls[1].Id));
            _screenings.Add(new ScreeningRecord(Guid.NewGuid(), "Film 3", FilmGenre.Drama, 2019, DateTime.Now.AddHours(3), 150, _cinemaHalls[2].Id));
            _screenings.Add(new ScreeningRecord(Guid.NewGuid(), "Film 4", FilmGenre.Horror, 2022, DateTime.Now.AddHours(4), 110, _cinemaHalls[0].Id));
            _screenings.Add(new ScreeningRecord(Guid.NewGuid(), "Film 5", FilmGenre.ScienceFiction, 2023, DateTime.Now.AddHours(5), 130, _cinemaHalls[1].Id));
            _screenings.Add(new ScreeningRecord(Guid.NewGuid(), "Film 6", FilmGenre.Romance, 2018, DateTime.Now.AddHours(6), 100, _cinemaHalls[2].Id));
            _screenings.Add(new ScreeningRecord(Guid.NewGuid(), "Film 7", FilmGenre.Thriller, 2024, DateTime.Now.AddHours(7), 140, _cinemaHalls[0].Id));
            #endregion
        }


        public async IAsyncEnumerable<CinemaHallDBModel> GetCinemaHallsAsync()
        {
            foreach (var cinemaHall in _cinemaHalls)
            {
                await Task.Delay(1000);
                yield return new CinemaHallDBModel(cinemaHall.Id, cinemaHall.Name, cinemaHall.Capacity, cinemaHall.Type, cinemaHall.rowCount);
            }
        }

        public Task<IEnumerable<ScreeningDBModel>> GetScreeningsByCinemaHallAsync(Guid cinemaHallId)
        {
            return Task.Run(() => {
                Thread.Sleep(1000);
                return _screenings.Where(screening => screening.cinemaHallId == cinemaHallId).Select(screening => new ScreeningDBModel(screening.Id, screening.name, screening.genre, screening.yearOfRelease, screening.launchTime, screening.duration, screening.cinemaHallId));

            }); 
        }

        public Task<int> GetScreeningsCountByCinemaHallAsync(Guid cinemaHallId)
        {
            return Task.Run(() => {
                Thread.Sleep(500);
                return _screenings.Count(screening => screening.cinemaHallId == cinemaHallId);
            });
        }

        public Task<CinemaHallDBModel> GetCinemaHallAsync(Guid id)
        {
            return Task.Run(() => {
                Thread.Sleep(1000);
                var cinemaHall = _cinemaHalls.FirstOrDefault(ch => ch.Id == id);
                if (cinemaHall == null)
                    return null;
                return new CinemaHallDBModel(cinemaHall.Id, cinemaHall.Name, cinemaHall.Capacity, cinemaHall.Type, cinemaHall.rowCount);
            });
        }

        public Task<ScreeningDBModel> GetScreeningAsync(Guid id)
        {
            return Task.Run(() => {
                var screening = _screenings.FirstOrDefault(screening => screening.Id == id);
                return screening is null ? null : new ScreeningDBModel(screening.Id, screening.name, screening.genre, screening.yearOfRelease, screening.launchTime, screening.duration, screening.cinemaHallId);
                });
        }

        public Task SaveScreeningAsync(ScreeningDBModel screening)
        {
            throw new NotImplementedException();
        }

        public Task DeleteScreeningAsync(Guid screeningId)
        {
            throw new NotImplementedException();
        }
    }
}
