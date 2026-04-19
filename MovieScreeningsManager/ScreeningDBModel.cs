using MovieScreeningsManager.Common.Enums;
using SQLite;
using System.Xml.Linq;

namespace MovieScreeningsManager.DBModels
{
    public class ScreeningDBModel
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public Guid CinemaHallId { get; set; }
        public string Name { get; set; }
        public FilmGenre Genre { get; set; }
        public int YearOfRelease { get; set; }
        public DateTime LaunchTime { get; set; }
        public int Duration { get; set; }

        public ScreeningDBModel(Guid id, string name, FilmGenre genre, int yearOfRelease, DateTime launchTime, int duration, Guid cinemaHallId) 
        {
            Id = id;
            Name = name;
            Genre = genre;
            YearOfRelease = yearOfRelease;
            LaunchTime = launchTime;
            Duration = duration;
            CinemaHallId = cinemaHallId;
        }
        public ScreeningDBModel(string name, FilmGenre genre, int yearOfRelease, DateTime launchTime, int duration, Guid cinemaHallId) : this(Guid.NewGuid(), name, genre, yearOfRelease, launchTime, duration, cinemaHallId)
        {
            
        }

        public ScreeningDBModel()
        {

        }
    }
}