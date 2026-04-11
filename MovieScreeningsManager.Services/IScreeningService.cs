using MovieScreeningsManager.DTOModels.Screenings;

namespace MovieScreeningsManager.Services
{
    public interface IScreeningService
    {
        IEnumerable<ScreeningListDTO> GetScreeningsByCinemaHall(Guid cinemaHallId);
        ScreeningDetailsDTO GetScreening(Guid id);
    }
}
