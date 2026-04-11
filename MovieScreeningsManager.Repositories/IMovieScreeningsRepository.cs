using MovieScreeningsManager.DBModels;

namespace MovieScreeningsManager.Repositories
{
    public interface IMovieScreeningsRepository
    {
        IEnumerable<ScreeningDBModel> GetScreeningsByCinemaHall(Guid cinemaHallId);
        int GetScreeningsCountByCinemaHall(Guid cinemaHallId);
        ScreeningDBModel GetScreening(Guid id);
    }
}
