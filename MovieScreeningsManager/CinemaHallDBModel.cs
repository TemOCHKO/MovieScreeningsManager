using MovieScreeningsManager.Common.Enums;
using SQLite;

namespace MovieScreeningsManager.DBModels
{

    public class CinemaHallDBModel
    {

        [PrimaryKey]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public int Capacity { get; set; }
        public CinemaHallType Type { get; set; }
        public int RowCount { get; set; }
        public CinemaHallDBModel(string name, int capacity, CinemaHallType type, int rowCount) : this(Guid.NewGuid(), name, capacity, type, rowCount)
        {
            
        }

        public CinemaHallDBModel()
        {

        }
        public CinemaHallDBModel(Guid id, string name, int capacity, CinemaHallType type, int rowCount)
        {
            Id = id;
            Name = name;
            Capacity = capacity;
            Type = type;
            RowCount = rowCount;
        }
    }
}
