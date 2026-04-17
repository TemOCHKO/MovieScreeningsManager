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

        public async Task<CinemaHallDetailsDTO> GetCinemaHallAsync(Guid cinemaHallid)
        {
            var cinemaHall = await _cinemaHallRepository.GetCinemaHallAsync(cinemaHallid);
            if (cinemaHall == null)
                return null;
            var movieScreeningsCount = await _movieScreeningsRepository.GetScreeningsCountByCinemaHallAsync(cinemaHallid);
            return new  CinemaHallDetailsDTO(cinemaHall.Id, cinemaHall.Name, cinemaHall.Capacity, cinemaHall.Type, movieScreeningsCount, cinemaHall.RowCount);
        }


        public async IAsyncEnumerable<CinemaHallListDTO> GetCinemaHallsAsync()
        {
            await foreach (var cinema in _cinemaHallRepository.GetCinemaHallsAsync())
            {
                var movieScreeningsCount = await _movieScreeningsRepository.GetScreeningsCountByCinemaHallAsync(cinema.Id);
                yield return new CinemaHallListDTO(cinema.Id, cinema.Name, cinema.Capacity, movieScreeningsCount);
            }
        }
    }
}
