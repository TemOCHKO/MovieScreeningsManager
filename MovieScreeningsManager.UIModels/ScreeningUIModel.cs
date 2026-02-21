using MovieScreeningsManager.Common.Enums;
using MovieScreeningsManager.DBModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieScreeningsManager.UIModels
{
    public class ScreeningUIModel
    {
        private ScreeningDBModel _dbModel;
        private string _name;
        private FilmGenre _genre;
        private int _yearOfRelease;
        private DateTime _launchTime;
        private int _duration;

        public Guid Id { get => _dbModel.Id; }
        public Guid CinemaHallId { get => _dbModel.CinemaHallId; }
        public string Name 
        {
            get => _name; 
            set => _name = value; 
        }
        public FilmGenre Genre 
        { 
            get => _genre; 
            set => _genre = value; 
        }
        public int YearOfRelease 
        {
            get => _yearOfRelease;
            set => _yearOfRelease = value;
        }
        public DateTime LaunchTime 
        {
            get => _launchTime;
            set => _launchTime = value;
        }
        public int Duration 
        { 
            get => _duration;
            set => _duration = value;
        }
        public DateTime? EndTime { get; set; }

        public ScreeningUIModel() { }

        public ScreeningUIModel(ScreeningDBModel dbModel)
        {
            _dbModel = dbModel;
            _name = dbModel.Name;
            _genre = dbModel.Genre;
            _yearOfRelease = dbModel.YearOfRelease;
            _launchTime = dbModel.LaunchTime;
            _duration = dbModel.Duration;
        }

    }
}
