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

        // Fields to hold the data of the screening.
        // These fields are used to store the data that is
        // displayed in the UI and can be modified by the user.
        // The Id and CinemaHallId are read-only,
        // as they should not be changed after creation.
        private Guid _id;
        private Guid _cinemaHallId;
        private string _name;
        private FilmGenre _genre;
        private int _yearOfRelease;
        private DateTime _launchTime;
        private int _duration;
        private DateTime? _endTime;


        // Properties to expose the fields of the UI model.
        // The ID and CinemaHallId are read-only,
        // as they should not be changed after creation.

        // Id is generated with the corresponding DB model
        public Guid Id { get => _id; }
        public Guid CinemaHallId { get => _cinemaHallId; }
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
        public DateTime? EndTime 
        { 
            get => _endTime;
            private set => _endTime = value;
        }

        // A constructor for creating a new screening without an existing DB model

        public ScreeningUIModel(Guid cinemaHallID)
        {
            _cinemaHallId = cinemaHallID;
        }

        // A constructor for creating a ScreeningUIModel based on an existing ScreeningDBModel
        public ScreeningUIModel(ScreeningDBModel dbModel)
        {
            _dbModel = dbModel;
            _name = dbModel.Name;
            _genre = dbModel.Genre;
            _yearOfRelease = dbModel.YearOfRelease;
            _launchTime = dbModel.LaunchTime;
            _duration = dbModel.Duration;
            calculateEndTime();
        }

        /// <summary>
        /// Function to calculate the end time of the screening based on the launch time and duration. This is necessary because the 
        /// end time is not stored in the database, but is calculated when needed.
        /// </summary>
        private void calculateEndTime()
        {
            _endTime = _launchTime.AddMinutes(_duration);
        }

        /// <summary>
        /// Saves changes tht have been made to UI model straight to DB Model. If Db Model does not exist yet,
        /// it creates a new one and assigns it an ID. 
        /// </summary>
        public void SaveChangesToDBModel()
        {
            if (_dbModel == null)
            {
                _dbModel = new ScreeningDBModel(_name, _genre, _yearOfRelease, _launchTime, _duration, _cinemaHallId);
                _id = _dbModel.Id;
            }
            else
            {
                _dbModel.Name = _name;
                _dbModel.Genre = _genre;
                _dbModel.YearOfRelease = _yearOfRelease;
                _dbModel.LaunchTime = _launchTime;
                _dbModel.Duration = _duration;
            }

        }

        public override string ToString()
        {
            return $"Screening: {Name}, Genre: {Genre}, Year: {YearOfRelease}, Launch Time: {LaunchTime}, Duration: {Duration} minutes, End Time: {EndTime}";
        }
    }
}
