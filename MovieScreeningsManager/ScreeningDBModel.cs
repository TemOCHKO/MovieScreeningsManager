using MovieScreeningsManager.Common.Enums;

namespace MovieScreeningsManager.DBModels
{
    public class ScreeningDBModel
    {
        public Guid Id { get; }
        public Guid CinemaHallId { get;}
        public string Name { get; set; }
        public FilmGenre Genre { get; set; }
        public int YearOfRelease { get; set; }
        public DateTime LaunchTime { get; set; }
        public int Duration { get; set; }
        public DateTime EndTime { get; set; }

        private ScreeningDBModel() { }
        public ScreeningDBModel(string name, FilmGenre genre, int yearOfRelease, DateTime launchTime, int duration, Guid cinemaHallId)
        {
            Id = Guid.NewGuid();
            Name = name;
            Genre = genre;
            YearOfRelease = yearOfRelease;
            LaunchTime = launchTime;
            Duration = duration;
            CinemaHallId = cinemaHallId;
        }
    }
}