using MovieScreeningsManager.DBModels;
using MovieScreeningsManager.Storage;

namespace MovieScreeningsManager.Repositories
{
    public class MovieScreeningsRepository : IMovieScreeningsRepository
    {
        private readonly IStorageContext _storageContext;
        public MovieScreeningsRepository(IStorageContext storageContext)
        {
            _storageContext = storageContext;
        }

        public IEnumerable<ScreeningDBModel> GetScreeningsByCinemaHall(Guid cinemaHallId)
        {
            return _storageContext.GetScreeningsByCinemaHall(cinemaHallId);
        }

        public int GetScreeningsCountByCinemaHall(Guid cinemaHallId)
        {
            return _storageContext.GetScreeningsCountByCinemaHall(cinemaHallId);
        }

        public ScreeningDBModel GetScreening(Guid id)
        {
            return _storageContext.GetScreening(id);
        }
    }
}
