using MovieScreeningsManager.Common.Enums;
using MovieScreeningsManager.DTOModels.Screenings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MovieScreeningsManager.Services
{
    public static class Validator
    {
        public record struct ValidationError(string errorMessage, string memberName);

        public static List<ValidationError> Validate(this ScreeningEditDTO screeningCandidate)
        {
            var errors = new List<ValidationError>();
            if (screeningCandidate.CinemaHallId == Guid.Empty)
                errors.Add(new ValidationError("Screening must be assigned to a cinema hall.", nameof(ScreeningEditDTO.CinemaHallId)));
            errors.AddRange(ValidateScreening(screeningCandidate.Name, screeningCandidate.FilmGenre, screeningCandidate.LaunchTime, screeningCandidate.Duration, screeningCandidate.YearOfRelease));
            return errors;
        }

        public static List<ValidationError> Validate(this ScreeningCreateDTO screeningCandidate)
        {
            var errors = new List<ValidationError>();
            if (screeningCandidate.CinemaHallId == Guid.Empty)
                errors.Add(new ValidationError("Screening must be assigned to a cinema hall.", nameof(ScreeningCreateDTO.CinemaHallId)));
            errors.AddRange(ValidateScreening(screeningCandidate.Name, screeningCandidate.FilmGenre, screeningCandidate.LaunchTime, screeningCandidate.Duration, screeningCandidate.YearOfRelease));
            return errors;
        }

        public static List<ValidationError> ValidateScreening(string title, FilmGenre? genre, DateTime? date, int duration, int yearOfRelease)
        {
            var errors = new List<ValidationError>();
            errors.AddRange(ValidateMovieTitle(title, nameof(ScreeningCreateDTO.Name), "Title"));
            errors.AddRange(ValidateDate(date, nameof(ScreeningCreateDTO.LaunchTime), "Launch Time"));
            if (genre == null)
            {
                errors.Add(new ValidationError("Genre must be selected.", nameof(ScreeningCreateDTO.FilmGenre)));
            }
            errors.AddRange(ValidateDuration(duration, nameof(ScreeningCreateDTO.Duration), "Duration"));
            errors.AddRange(ValidateYear(yearOfRelease, nameof(ScreeningCreateDTO.YearOfRelease), "Year of Release"));
            return errors;
        }

        private static List<ValidationError> ValidateMovieTitle(string name, string propertyName, string displayName)
        {
            var errors = new List<ValidationError>();
            if (String.IsNullOrWhiteSpace(name))
            {
                errors.Add(new ValidationError($"{displayName} can't be empty.", propertyName));
                return errors;
            }
            if (name.Length < 2)
                errors.Add(new ValidationError($"{displayName} must be at least 2 caracters long.", propertyName));
            if (!(!string.IsNullOrWhiteSpace(name) && Regex.IsMatch(name, @"^[\p{L}\s]+$")))
                errors.Add(new ValidationError($"{displayName} must consist only from letters.", propertyName));
            return errors;
        }

        private static List<ValidationError> ValidateDate(DateTime? date, string propertyName, string displayName)
        {
            var errors = new List<ValidationError>();
            if (date == null)
                errors.Add(new ValidationError($"{displayName}  must be selected.", propertyName));
            if (date <= DateTime.Today)
                errors.Add(new ValidationError($"{displayName}  cannot be in past.", propertyName));
            return errors;
        }

        private static List<ValidationError> ValidateDuration(int? duration, string propertyName, string displayName)
        {
            var errors = new List<ValidationError>();
            if (duration == null)
                errors.Add(new ValidationError($"{displayName} must be specified.", propertyName));
            int dur;
            if (!int.TryParse(duration.ToString(), out dur))
                errors.Add(new ValidationError($"{displayName} must be a number", propertyName));
            if (duration <= 0)
                errors.Add(new ValidationError($"{displayName} must be greater than zero.", propertyName));
            return errors;
        }

        private static List<ValidationError> ValidateYear(int? year, string propertyName, string displayName)
        {
            var errors = new List<ValidationError>();
            if (year == null)
                errors.Add(new ValidationError($"{displayName} must be specified.", propertyName));
            int y;
            if (!int.TryParse(year.ToString(), out y))
                errors.Add(new ValidationError($"{displayName} must be a number.", propertyName));
            if (year <= 1900 || year > DateTime.Now.Year)
                errors.Add(new ValidationError($"{displayName} must be a valid year.", propertyName));
            return errors;
        }
    }
}
