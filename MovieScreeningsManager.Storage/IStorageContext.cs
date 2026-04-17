using MovieScreeningsManager.DBModels;

namespace MovieScreeningsManager.Storage
{
    public interface IStorageContext
    {
        IAsyncEnumerable<CinemaHallDBModel> GetCinemaHallsAsync();
        Task<CinemaHallDBModel> GetCinemaHallAsync(Guid id);
        Task<IEnumerable<ScreeningDBModel>> GetScreeningsByCinemaHallAsync(Guid cinemaHallId);
        Task<ScreeningDBModel> GetScreeningAsync(Guid id);
        Task<int> GetScreeningsCountByCinemaHallAsync(Guid cinemaHallId);

    }
}
