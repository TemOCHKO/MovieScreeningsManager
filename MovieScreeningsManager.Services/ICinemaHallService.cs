using MovieScreeningsManager.DTOModels.CinemaHalls;

namespace MovieScreeningsManager.Services
{
    public interface ICinemaHallService
    {
        IAsyncEnumerable<CinemaHallListDTO> GetCinemaHallsAsync();
        Task<CinemaHallDetailsDTO> GetCinemaHallAsync(Guid cinemaHallid);
        Task DeleteCinemaHallAsync(Guid cinemaHallId);
       
    }
}
