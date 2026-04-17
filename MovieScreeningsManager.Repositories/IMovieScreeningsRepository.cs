using MovieScreeningsManager.DBModels;

namespace MovieScreeningsManager.Repositories
{
    public interface IMovieScreeningsRepository
    {
        Task<IEnumerable<ScreeningDBModel>> GetScreeningsByCinemaHallAsync(Guid cinemaHallId);
        Task<int> GetScreeningsCountByCinemaHallAsync(Guid cinemaHallId);
        Task<ScreeningDBModel> GetScreeningAsync(Guid id);
    }
}
