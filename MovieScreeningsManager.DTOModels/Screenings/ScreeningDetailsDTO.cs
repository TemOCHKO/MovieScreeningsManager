using MovieScreeningsManager.Common.Enums;

namespace MovieScreeningsManager.DTOModels.Screenings
{
    public class ScreeningDetailsDTO
    {
        public Guid Id { get; }
        public string Name { get;}
        public DateTime LaunchTime { get; }
        public DateTime EndTime { get; }
        public FilmGenre FilmGenre { get; }
        public int Duration { get; }
        public int YearOfRelease { get; }

        public ScreeningDetailsDTO(Guid id, string name, DateTime launchTime, int duration, FilmGenre filmGenre, int yearOfRelease)
        {
            Id = id;
            Name = name;
            LaunchTime = launchTime;
            Duration = duration;
            FilmGenre = filmGenre;
            YearOfRelease = yearOfRelease;
        }
    }
}
