using MovieScreeningsManager.Common.Enums;

namespace MovieScreeningsManager.DBModels
{

    public class CinemaHallDBModel
    {
        public static int InstanceCount { get; private set; } = 0;

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
    }
}
