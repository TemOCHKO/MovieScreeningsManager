using MovieScreeningsManager.DBModels;
using MovieScreeningsManager.DTOModels.CinemaHalls;
using MovieScreeningsManager.Repositories;
using System.ComponentModel.DataAnnotations;

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

        public async Task CreateCinemaHallAsync(CinemaHallCreateDTO cinemaHall)
        {
            await _cinemaHallRepository.CreateCinemaHallAsync(new CinemaHallDBModel
            {
                Id = cinemaHall.Id,
                Name = cinemaHall.Name,
                Capacity = cinemaHall.Capacity,
                Type = cinemaHall.Type,
                RowCount = cinemaHall.RowCount
            });
        }

        public Task DeleteCinemaHallAsync(Guid cinemaHallId)
        {
            return _cinemaHallRepository.DeleteCinemaHallAsync(cinemaHallId);
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

        public async Task UpdateCinemaHallAsync(CinemaHallEditDTO cinemaHall)
        {

            var errors = cinemaHall.Validate();
            if (errors.Count > 0)
                throw new ValidationException(String.Join(Environment.NewLine, errors.Select(s => s.errorMessage)));
            await _cinemaHallRepository.UpdateCinemaHallAsync(new CinemaHallDBModel
            {
                Id = cinemaHall.Id,
                Name = cinemaHall.Name,
                Capacity = cinemaHall.Capacity,
                Type = cinemaHall.Type,
                RowCount = cinemaHall.RowCount
            });
        }
    }
}
