using MovieScreeningsManager.Common.Enums;

namespace MovieScreeningsManager.DBModels
{
    public class ScreeningDBModel
    {
        public Guid Id { get; set; }
        public Guid CinemaHallId { get; set; }
        public string Name { get; set; }
        public FilmGenre Genre { get; set; }
        public int YearOfRelease { get; set; }
        public DateTime LaunchTime { get; set; }
        public int Duration { get; set; }
        public DateTime EndTime { get; set; }
    }
}