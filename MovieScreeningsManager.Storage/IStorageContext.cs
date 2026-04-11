using MovieScreeningsManager.DBModels;

namespace MovieScreeningsManager.Storage
{
    public interface IStorageContext
    {
        IEnumerable<CinemaHallDBModel> GetCinemaHalls();
        CinemaHallDBModel GetCinemaHall(Guid id);
        IEnumerable<ScreeningDBModel> GetScreeningsByCinemaHall(Guid cinemaHallId);
        ScreeningDBModel GetScreening(Guid id);
        int GetScreeningsCountByCinemaHall(Guid cinemaHallId);

    }
}
