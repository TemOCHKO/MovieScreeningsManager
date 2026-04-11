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
        public IEnumerable<ScreeningListDTO> GetScreeningsByCinemaHall(Guid cinemaHallId)
        {
            foreach (var screening in _movieScreeningRepository.GetScreeningsByCinemaHall(cinemaHallId))
            {
                yield return new ScreeningListDTO(screening.Id, screening.Name, screening.LaunchTime, screening.Duration);
            }
        }
     
        public ScreeningDetailsDTO GetScreening(Guid id)
        {
            var screening = _movieScreeningRepository.GetScreening(id);
            return screening is null ? null : new ScreeningDetailsDTO(screening.Id, screening.Name, screening.LaunchTime, screening.Duration, screening.Genre, screening.YearOfRelease);
        }

       
    }
}
