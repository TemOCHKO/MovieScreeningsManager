using MovieScreeningsManager.Common.Enums;
using MovieScreeningsManager.DBModels;
using MovieScreeningsManager.Services;

namespace MovieScreeningsManager.UIModels
{
    public class CinemaHallUIModel
    {
        private CinemaHallDBModel _dbModel;
        private Guid? _id;
        private string _name;
        private int _capacity;
        private CinemaHallType _type;
        private List<ScreeningUIModel> _screenings;

        public Guid? Id { get => _dbModel.Id; }
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
        public IReadOnlyList<ScreeningUIModel> Screenings 
        {
            get => _screenings; 
        }

        public TimeSpan TotalScreeningsTime { get; private set; }

        public CinemaHallUIModel()
        {
            _screenings = new List<ScreeningUIModel>();
            calculateTotalScreeningsTime();
        }

        public CinemaHallUIModel(CinemaHallDBModel dbModel) : this()
        {
            _dbModel = dbModel;
            _id = dbModel.Id;
            _name = dbModel.Name;
            _capacity = dbModel.Capacity;
            _type = dbModel.Type;
        }

        private void calculateTotalScreeningsTime()
        {
            TimeSpan totalTime = TimeSpan.Zero;
            foreach (var screening in _screenings)
            {
                totalTime = totalTime.Add(TimeSpan.FromMinutes(screening.Duration));
            }
            TotalScreeningsTime = totalTime;
        }

        public void SaveChangesToDBModel()
        {
            if (_dbModel == null)
            {
                _dbModel = new CinemaHallDBModel(_name, _capacity, _type);
                _id = _dbModel.Id;
            }
            else
            {
                _dbModel.Name = _name;
                _dbModel.Capacity = _capacity;
                _dbModel.Type = _type;
            }
        }

        public void LoadScreenings(StorageService screeningDBModels)
        {
            if (Id == null || _screenings.Count > 0) return;

            foreach (var screeningDBModel in screeningDBModels.GetScreenings(Id.Value))
            {
                _screenings.Add(new ScreeningUIModel(screeningDBModel));
            }
        }

        public override string ToString()
        {
            return $"Cinema Hall: {Name}, Capacity: {Capacity}, Type: {Type}, Total Screenings Time: {TotalScreeningsTime}";
        }
    }
}
