using MovieScreeningsManager.DBModels;

namespace MovieScreeningsManager.Repositories
{
    public interface ICinemaHallRepository
    {
        IAsyncEnumerable<CinemaHallDBModel> GetCinemaHallsAsync();
        Task<CinemaHallDBModel> GetCinemaHallAsync(Guid id);
    }
}
