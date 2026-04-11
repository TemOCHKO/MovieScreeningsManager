using MovieScreeningsManager.DTOModels.CinemaHalls;
using MovieScreeningsManager.Repositories;

namespace MovieScreeningsManager.Services
{
    public class CinemaHallService : ICinemaHallService
    {
        private readonly ICinemaHallRepository _cinemaHallRepository;
        private readonly IMovieScreeningsRepository _movieScreeningsRepository;
        public CinemaHallService(ICinemaHallRepository cinemaHallRepository, IMovieScreeningsRepository movieScreeningsRepository)
        {
            _cinemaHallRepository = cinemaHallRepository;
            _movieScreeningsRepository = movieScreeningsRepository;
        }

        public CinemaHallDetailsDTO GetCinemaHall(Guid cinemaHallid)
        {
            var cinemaHall = _cinemaHallRepository.GetCinemaHall(cinemaHallid);
            if (cinemaHall == null)
                return null;
            return new  CinemaHallDetailsDTO(cinemaHall.Id, cinemaHall.Name, cinemaHall.Capacity, cinemaHall.Type, _movieScreeningsRepository.GetScreeningsCountByCinemaHall(cinemaHallid), cinemaHall.RowCount);
        }


        public IEnumerable<CinemaHallListDTO> GetCinemaHalls()
        {
            foreach (var cinema in _cinemaHallRepository.GetCinemaHalls())
            {
                var movieScreeningsCount = _movieScreeningsRepository.GetScreeningsCountByCinemaHall(cinema.Id);
                yield return new CinemaHallListDTO(cinema.Id, cinema.Name, cinema.Capacity, movieScreeningsCount);
            }
        }
    }
}
