namespace MovieScreeningsManager.DTOModels.CinemaHalls
{
    public class CinemaHallListDTO
    {
        public Guid Id { get; }
        public string Name { get;}
        public int Capacity { get; }
        public int MovieScreeningsCount { get; }

        public CinemaHallListDTO(Guid id, string name, int capacity, int movieScreeningsCount)
        {
            Id = id;
            Name = name;
            Capacity = capacity;
            MovieScreeningsCount = movieScreeningsCount;
        }
    }
}
