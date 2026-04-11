namespace MovieScreeningsManager.DTOModels.Screenings
{
    public class ScreeningListDTO
    {
        public Guid Id { get; }
        public string Name { get; }
        public DateTime LaunchTime { get; }
        public int Duration { get; }

        public ScreeningListDTO(Guid id, string name, DateTime launchTime, int duration)
        {
            Id = id;
            Name = name;
            LaunchTime = launchTime;
            Duration = duration;

        }
    }
}
