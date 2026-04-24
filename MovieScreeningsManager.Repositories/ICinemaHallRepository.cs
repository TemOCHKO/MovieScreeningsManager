using MovieScreeningsManager.DBModels;

namespace MovieScreeningsManager.Repositories
{
    public interface ICinemaHallRepository
    {
        IAsyncEnumerable<CinemaHallDBModel> GetCinemaHallsAsync();
        Task<CinemaHallDBModel> GetCinemaHallAsync(Guid id);
        Task DeleteCinemaHallAsync(Guid id);
        Task UpdateCinemaHallAsync(CinemaHallDBModel cinemaHall);
        Task CreateCinemaHallAsync(CinemaHallDBModel cinemaHall);
    }
}
