using MovieScreeningsManager.DTOModels.CinemaHalls;

namespace MovieScreeningsManager.Services
{
    public interface ICinemaHallService
    {
        IEnumerable<CinemaHallListDTO> GetCinemaHalls();
        CinemaHallDetailsDTO GetCinemaHall(Guid cinemaHallid);
    }
}
