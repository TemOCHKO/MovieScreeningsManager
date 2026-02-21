using MovieScreeningsManager.Common.Enums;
using MovieScreeningsManager.DBModels;

namespace MovieScreeningsManager.UIModels
{
    public class CinemaHallUIModel
    {
        private CinemaHallDBModel _dbModel;
        private string _name;
        private int _capacity;
        private CinemaHallType _type;
        private IReadOnlyCollection<ScreeningUIModel> _screenings;

        public Guid Id { get => _dbModel.Id; }
        public string Name 
        {
            get => _name;
            set => _name = value;
        }
        public int Capacity 
        {
            get => _capacity; 
            set => _capacity = value; 
        }
        public CinemaHallType Type 
        { 
            get => _type; 
            set => _type = value; 
        }
        public IReadOnlyCollection<ScreeningUIModel> Screenings 
        {
            get => _screenings; 
        }

        public TimeSpan TotalScreeningsTime { get; set; }

        public CinemaHallUIModel()
        {
            _screenings = new List<ScreeningUIModel>();
        }

        public CinemaHallUIModel(CinemaHallDBModel dbModel) : this()
        {
            _dbModel = dbModel;
            _name = dbModel.Name;
            _capacity = dbModel.Capacity;
            _type = dbModel.Type;
        }
    }
}
