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

        public CinemaHallDBModel GetCinemaHall(Guid id)
        {
            return _storageContext.GetCinemaHall(id);
        }

        public IEnumerable<CinemaHallDBModel> GetCinemaHalls()
        {
            return _storageContext.GetCinemaHalls();
        }
    }
}
