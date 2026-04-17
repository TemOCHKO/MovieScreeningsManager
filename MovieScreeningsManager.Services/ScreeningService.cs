using MovieScreeningsManager.DTOModels.Screenings;
using MovieScreeningsManager.Repositories;

namespace MovieScreeningsManager.Services
{
    public class ScreeningService : IScreeningService
    {
        private readonly IMovieScreeningsRepository _movieScreeningRepository;
        public ScreeningService(IMovieScreeningsRepository movieScreeningsRepository)
        {
            _movieScreeningRepository = movieScreeningsRepository;
        }
        public async Task<IEnumerable<ScreeningListDTO>> GetScreeningsByCinemaHallAsync(Guid cinemaHallId)
        {
            return (await _movieScreeningRepository.GetScreeningsByCinemaHallAsync(cinemaHallId))
                .Select(screening => new ScreeningListDTO(screening.Id, screening.Name, screening.LaunchTime, screening.Duration));
        }
     
        public async Task<ScreeningDetailsDTO> GetScreeningAsync(Guid id)
        {
            var screening = await _movieScreeningRepository.GetScreeningAsync(id);
            return screening is null ? null : new ScreeningDetailsDTO(screening.Id, screening.Name, screening.LaunchTime, screening.Duration, screening.Genre, screening.YearOfRelease);
        }

       
    }
}
