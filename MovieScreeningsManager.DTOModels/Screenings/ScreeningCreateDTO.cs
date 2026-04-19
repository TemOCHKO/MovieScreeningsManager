using MovieScreeningsManager.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieScreeningsManager.DTOModels.Screenings
{
    public class ScreeningCreateDTO
    {
        public Guid CinemaHallId { get; }
        public string Name { get; }
        public DateTime LaunchTime { get; }
        //public DateTime EndTime { get; }
        public FilmGenre FilmGenre { get; }
        public int Duration { get; }
        public int YearOfRelease { get; }

        public ScreeningCreateDTO(Guid cinemaHallId, string name, DateTime launchTime, int duration, FilmGenre filmGenre, int yearOfRelease)
        {
            CinemaHallId = cinemaHallId;
            Name = name;
            LaunchTime = launchTime;
            Duration = duration;
            FilmGenre = filmGenre;
            YearOfRelease = yearOfRelease;
        }
    
    }
}
