using MovieScreeningsManager.DBModels;

namespace MovieScreeningsManager.Services
{
    internal static class FakeStorage
    {
        private static readonly List<CinemaHallDBModel> _cinemaHalls;
        private static readonly List<ScreeningDBModel> _screenings;
        
        internal static IEnumerable<CinemaHallDBModel> CinemaHalls
        {
            get
            {
                return _cinemaHalls.ToList();
            }
        } 

        internal static IEnumerable<ScreeningDBModel> Screenings
        {
            get
            {
                return _screenings.ToList();
            }
        }

        static FakeStorage()
        {
            _cinemaHalls = new List<CinemaHallDBModel>();
            _screenings = new List<ScreeningDBModel>();

            // Adding 3 different cinema halls to the fake storage
            var cinemaHall3D = new CinemaHallDBModel("3D Cinema Hall", 100, Common.Enums.CinemaHallType.ThreeD);
            var cinemaHallIMAX = new CinemaHallDBModel("IMAX Cinema Hall", 150, Common.Enums.CinemaHallType.IMAX);
            var cinemaHallStandard = new CinemaHallDBModel("Standard Cinema Hall", 80, Common.Enums.CinemaHallType.Standard);

            _cinemaHalls = new List<CinemaHallDBModel> { cinemaHall3D,
                cinemaHallIMAX,
                cinemaHallStandard
            };

            // Adding 12 different screenings to the fake storage
            var screening1 = new ScreeningDBModel("Inception", Common.Enums.FilmGenre.ScienceFiction, 2010, DateTime.Now.AddHours(2), 148, cinemaHallIMAX.Id);
            var screening2 = new ScreeningDBModel("The Dark Knight", Common.Enums.FilmGenre.Action, 2008, DateTime.Now.AddHours(4), 152, cinemaHallIMAX.Id);
            var screening3 = new ScreeningDBModel("Interstellar", Common.Enums.FilmGenre.ScienceFiction, 2014, DateTime.Now.AddHours(6), 169, cinemaHallIMAX.Id);
            var screening4 = new ScreeningDBModel("The Matrix", Common.Enums.FilmGenre.ScienceFiction, 1999, DateTime.Now.AddHours(8), 136, cinemaHallIMAX.Id);
            var screening5 = new ScreeningDBModel("Gladiator", Common.Enums.FilmGenre.Action, 2000, DateTime.Now.AddHours(10), 155, cinemaHallIMAX.Id);
            var screening6 = new ScreeningDBModel("The Lord of the Rings: The Fellowship of the Ring", Common.Enums.FilmGenre.Fantasy, 2001, DateTime.Now.AddHours(12), 178, cinemaHallIMAX.Id);
            var screening7 = new ScreeningDBModel("The Lord of the Rings: The Two Towers", Common.Enums.FilmGenre.Fantasy, 2002, DateTime.Now.AddHours(14), 179, cinemaHallIMAX.Id);
            var screening8 = new ScreeningDBModel("The Lord of the Rings: The Return of the King", Common.Enums.FilmGenre.Fantasy, 2003, DateTime.Now.AddHours(16), 201, cinemaHallIMAX.Id);
            var screening9 = new ScreeningDBModel("The Avengers", Common.Enums.FilmGenre.Action, 2012, DateTime.Now.AddHours(18), 143, cinemaHallIMAX.Id);
            var screening10 = new ScreeningDBModel("Avatar", Common.Enums.FilmGenre.ScienceFiction, 2009, DateTime.Now.AddHours(20), 162, cinemaHallIMAX.Id);

            var screening11 = new ScreeningDBModel("Titanic", Common.Enums.FilmGenre.Romance, 1997, DateTime.Now.AddHours(22), 195, cinemaHallStandard.Id);
            var screening12 = new ScreeningDBModel("The Godfather", Common.Enums.FilmGenre.Crime, 1972, DateTime.Now.AddHours(24), 175, cinemaHallStandard.Id);

            _screenings = new List<ScreeningDBModel>
            {
                screening1,
                screening2,
                screening3,
                screening4,
                screening5,
                screening6,
                screening7,
                screening8,
                screening9,
                screening10,
                screening11,
                screening12
             };


        }
    }

}
