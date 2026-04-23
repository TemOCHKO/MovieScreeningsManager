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

        public Task<IEnumerable<ScreeningDBModel>> GetScreeningsByCinemaHallAsync(Guid cinemaHallId)
        {
            return _storageContext.GetScreeningsByCinemaHallAsync(cinemaHallId);
        }

        public Task<int> GetScreeningsCountByCinemaHallAsync(Guid cinemaHallId)
        {
            return _storageContext.GetScreeningsCountByCinemaHallAsync(cinemaHallId);
        }

        public Task<ScreeningDBModel> GetScreeningAsync(Guid id)
        {
            return _storageContext.GetScreeningAsync(id);
        }

        public Task SaveScreeningAsync(ScreeningDBModel screening)
        {
            return _storageContext.SaveScreeningAsync(screening);
        }

        public Task DeleteScreeningAsync(Guid screeningId)
        {
            return _storageContext.DeleteScreeningAsync(screeningId);
        }

        public Task UpdateScreeningAsync(ScreeningDBModel screening)
        {
            return _storageContext.UpdateScreeningAsync(screening);
        }
    }
}
