using MovieScreeningsManager.DBModels;
using MovieScreeningsManager.DTOModels.Screenings;
using MovieScreeningsManager.Repositories;
using System.ComponentModel.DataAnnotations;

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

        public async Task CreateScreeningAsync(ScreeningCreateDTO screening)
        {
            var errors = screening.Validate();
            if (errors.Count > 0)
                throw new ValidationException(String.Join(Environment.NewLine, errors.Select(s => s.errorMessage)));
            var newScreening = new ScreeningDBModel(screening.Name, screening.FilmGenre, screening.YearOfRelease, screening.LaunchTime, screening.Duration, screening.CinemaHallId);
            await _movieScreeningRepository.SaveScreeningAsync(newScreening);
        }

        public async Task DeleteScreeningAsync(Guid screeningId)
        {
            await _movieScreeningRepository.DeleteScreeningAsync(screeningId);
        }

        public async Task<ScreeningEditDTO> GetScreeningForEditAsync(Guid id)
        {
            var screening = await _movieScreeningRepository.GetScreeningAsync(id);
            return screening is null ? null : new ScreeningEditDTO(screening.Id, screening.CinemaHallId, screening.Name, screening.LaunchTime, screening.Duration, screening.Genre, screening.YearOfRelease);
        }

        public async Task UpdateScreeningAsync(ScreeningEditDTO screening)
        {
            var errors = screening.Validate();
            if (errors.Count > 0)
                throw new ValidationException(String.Join(Environment.NewLine, errors.Select(s => s.errorMessage)));
            var newScreening = new ScreeningDBModel(screening.Id, screening.Name, screening.FilmGenre, screening.YearOfRelease, screening.LaunchTime, screening.Duration, screening.CinemaHallId);
            await _movieScreeningRepository.UpdateScreeningAsync(newScreening);
        }
    }
}
