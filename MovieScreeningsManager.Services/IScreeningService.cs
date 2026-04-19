using MovieScreeningsManager.DBModels;
using MovieScreeningsManager.DTOModels.Screenings;

namespace MovieScreeningsManager.Services
{
    public interface IScreeningService
    {
        Task<IEnumerable<ScreeningListDTO>> GetScreeningsByCinemaHallAsync(Guid cinemaHallId);
        Task<ScreeningDetailsDTO> GetScreeningAsync(Guid id);
        Task CreateScreeningAsync(ScreeningCreateDTO screening);
        Task DeleteScreeningAsync(Guid screeningId);
    }
}
