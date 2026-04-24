using MovieScreeningsManager.DBModels;
using MovieScreeningsManager.Storage;

namespace MovieScreeningsManager.Repositories
{
    public class CinemaHallRepository : ICinemaHallRepository
    {

        // Readonly means the value is set only once when initialized. Then cannot set to null or modify it
        private readonly IStorageContext _storageContext;
        public CinemaHallRepository(IStorageContext storageContext)
        {
            _storageContext = storageContext;
        }

        public Task CreateCinemaHallAsync(CinemaHallDBModel cinemaHall)
        {
            return _storageContext.CreateCinemaHallAsync(cinemaHall);
        }

        public Task DeleteCinemaHallAsync(Guid id)
        {
            return _storageContext.DeleteCinemaHallAsync(id);
        }

        public Task<CinemaHallDBModel> GetCinemaHallAsync(Guid id)
        {
            return _storageContext.GetCinemaHallAsync(id);
        }

        public IAsyncEnumerable<CinemaHallDBModel> GetCinemaHallsAsync()
        {
            return _storageContext.GetCinemaHallsAsync();
        }

        public Task UpdateCinemaHallAsync(CinemaHallDBModel cinemaHall)
        {
            return _storageContext.UpdateCinemaHallAsync(cinemaHall);
        }
    }
}
