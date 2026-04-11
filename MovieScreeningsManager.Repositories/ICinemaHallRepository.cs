using MovieScreeningsManager.DBModels;

namespace MovieScreeningsManager.Repositories
{
    public interface ICinemaHallRepository
    {
        IEnumerable<CinemaHallDBModel> GetCinemaHalls();
        CinemaHallDBModel GetCinemaHall(Guid id);
    }
}
