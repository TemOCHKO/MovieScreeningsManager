using MovieScreeningsManager.Common.Enums;

namespace MovieScreeningsManager.DBModels
{

    public class CinemaHallDBModel
    {

        public Guid Id { get; }

        public string Name { get; set; }
        public int Capacity { get; set; }
        public CinemaHallType Type { get; set; }

        // Collection of screenings in this hall 
        public List<ScreeningDBModel> Screenings { get; set; }

        public TimeSpan TotalScreeningTime
        {
            get; set;
        }

        private CinemaHallDBModel() { }

        public CinemaHallDBModel(string name, int capacity, CinemaHallType type, List<ScreeningDBModel> screenings)
            {
                Id = Guid.NewGuid();
                Name = name;
                Capacity = capacity;
                Type = type;
                Screenings = screenings;
        }
    }
}
