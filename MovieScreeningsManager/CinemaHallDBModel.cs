using MovieScreeningsManager.Common.Enums;

namespace MovieScreeningsManager.DBModels
{

    public class CinemaHallDBModel
    {

        public Guid Id { get; }

        public string Name { get; set; }
        public int Capacity { get; set; }
        public CinemaHallType Type { get; set; }

        private CinemaHallDBModel() { }

        public CinemaHallDBModel(string name, int capacity, CinemaHallType type)
            {
                Id = Guid.NewGuid();
                Name = name;
                Capacity = capacity;
                Type = type;
        }
    }
}
