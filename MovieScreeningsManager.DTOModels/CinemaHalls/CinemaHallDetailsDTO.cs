using MovieScreeningsManager.Common.Enums;

namespace MovieScreeningsManager.DTOModels.CinemaHalls
{
    public class CinemaHallDetailsDTO
    {
        public Guid Id { get; }
        public string Name { get; }
        public int Capacity { get; }
        public CinemaHallType Type { get; }
        public int MovieScreeningsCount { get; }
        public int RowsCount { get; }
        public CinemaHallDetailsDTO(Guid id, string name, int capacity, CinemaHallType type, int movieScreeningsCount, int rowCount)
        {
            Id = id;
            Name = name;
            Capacity = capacity;
            Type = type;
            MovieScreeningsCount = movieScreeningsCount;
            RowsCount = rowCount;
        }
    }
}
