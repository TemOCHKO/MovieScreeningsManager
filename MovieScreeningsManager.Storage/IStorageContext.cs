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
        Task SaveScreeningAsync(ScreeningDBModel screening);
        Task DeleteScreeningAsync(Guid screeningId);
        Task DeleteCinemaHallAsync(Guid cinemaHallId);
        Task UpdateScreeningAsync(ScreeningDBModel screening);
        Task UpdateCinemaHallAsync(CinemaHallDBModel cinemaHall);
        Task CreateCinemaHallAsync(CinemaHallDBModel cinemaHall);

    }
}
